using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_ReturnActionResultType()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            
            var employeeController = new EmployeeController(employeeRepository.Object);

            var id = 1;
            var result = employeeController.DeleteEmployee(id);

            Assert.That(result, Is.TypeOf<RedirectResult>());
            Assert.That(result, Is.InstanceOf<ActionResult>());
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            
            var employeeController = new EmployeeController(employeeRepository.Object);

            var id = 1;
            employeeController.DeleteEmployee(id);

            employeeRepository.Verify(er => er.RemoveEmployee(id));
        }
    }
}