using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperHelperTests
    {
        private Mock<IHouseKeeperRepository> _houseKeeperRepository;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private Housekeeper _houseKeeper;
        private HousekeeperHelper _housekeeperHelper;
        private DateTime _sendingTime;

        [SetUp]
        public void Setup()
        {
            _houseKeeperRepository = new Mock<IHouseKeeperRepository>();
            _houseKeeper = new Housekeeper() { Email = "a", Oid = 1, FullName = "b", StatementEmailBody = "c" };

            _houseKeeperRepository.Setup(hkr => hkr.GetHousekeepers()).Returns(new List<Housekeeper> { _houseKeeper }.AsQueryable());
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            _sendingTime = new DateTime(2020, 9, 20);

            _housekeeperHelper = new HousekeeperHelper(_houseKeeperRepository.Object,
                                                            _statementGenerator.Object,
                                                            _emailSender.Object,
                                                            _messageBox.Object);

        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_EmialIsInvalid_ShouldNotGenerateStatement(string email)
        {
            _houseKeeper.Email = email;
            _housekeeperHelper.SendStatementEmails(_sendingTime);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _sendingTime), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_ShouldGenerateStatement()
        {
            _housekeeperHelper.SendStatementEmails(_sendingTime);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _sendingTime));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void StatementFileNameIsNullOrWhiteSpace_NoEmailCreateAndSending(string statementFileName)
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _sendingTime)).Returns(statementFileName);
            _housekeeperHelper.SendStatementEmails(_sendingTime);

            var subject = string.Format("Sandpiper Statement {0:yyyy-MM} {1}", _sendingTime, _houseKeeper.FullName);

            _emailSender.Verify(es => es.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody, statementFileName, subject), 
                                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailIsSent()
        {
            string statementFileName = "fileName";
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _sendingTime))
                                .Returns(statementFileName);
            _housekeeperHelper.SendStatementEmails(_sendingTime);
            _emailSender.Verify(es => es.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody, statementFileName, It.IsAny<string>()));
        }

        [Test]
        public void ExceptionHappenedWhenSendingEmail_ShowXtraMessage()
        {
            string statementFileName = "fileName";
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _sendingTime))
                                .Returns(statementFileName);
        
             _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                         .Throws<Exception>();

            _housekeeperHelper.SendStatementEmails(_sendingTime);
            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(),
                        MessageBoxButtons.OK));
        
        }
    }
}