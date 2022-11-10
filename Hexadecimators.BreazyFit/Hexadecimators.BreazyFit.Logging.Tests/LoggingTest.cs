using Hexadecimators.BreazyFit.SqlDataAccess;
using Hexadecimators.BreazyFit.Logging.Implementations;

namespace Hexadecimators.BreazyFit.Logging.Tests
{
    [TestClass]
    public class LoggingTest
    {
        [TestMethod]
        public void ShouldCreateNewLogger()
        {
            // Arrange
            var expected = typeof(Logger);
            var testString = "TestConnectionString";
            var dao = new LoggingDAO(testString);

            // Act
            var actual = new Logger(dao);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.GetType() == expected);
        }

    }
}