using Hexadecimators.BreazyFit.Registration.Implementations;

namespace Hexadecimators.BreazyFit.Registration.Tests
{
    [TestClass]
    internal class RegistrationTest
    {

        [TestMethod]
        public void TestException()

        {

            try
            {
                var reg = new Register();
                var res = reg.UserCreation("test@gmail.com", "Pass");
                Assert.Equals(res.IsSuccessful,true);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Account was not created.");
                return;
            }
            Assert.Fail("Nothing was thrown");

        }

        [TestMethod]
        public void ValidationShouldPass()
        {
            // Arrange
            var emailTest = "testmail@gmail.com";
            var passTest = "TestPassword123!";

            // Act
            {
                var reg = new Register();
                var actual = reg.UserCreation(emailTest, passTest);

                // Assert
                Assert.IsTrue(actual.IsSuccessful, "Is True");

            }


        }

        [TestMethod]
        public void ValidationShouldNotPass()
        {
            // Arrange
            var emailTest = "testm@ail@gmail.com";
            var passTest = "Test$^#%$";

            // Act
            {
                var reg = new Register();
                var actual = reg.UserCreation(emailTest, passTest);

                // Assert
                Assert.IsTrue(!actual.IsSuccessful, "Is False");

            }


        }

    }
}
