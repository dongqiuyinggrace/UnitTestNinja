using System;
using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        [Test]
        public void DownloadInstaller_FileIsSucessfullyDownloaded_ReturnTrue()
        {
            var fileDownloader = new Mock<IFileDownloader>();
            var installerHelper = new InstallerHelper();
            var result = installerHelper.DownloadInstaller("", "", fileDownloader.Object);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void DownloadInstaller_ExceptionHappenedWhenDownloading_ReturnFalse()
        {
            var fileDownloader = new Mock<IFileDownloader>();
            fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var installerHelper = new InstallerHelper();
            var result = installerHelper.DownloadInstaller("", "", fileDownloader.Object);

            Assert.That(result, Is.EqualTo(false));

        }
    }
}