using System.IO;
using System.Text;
using NUnit.Framework;

namespace KattisSolution.Tests
{
    [Ignore("Uncomment this only when there's no data files")]
    [TestFixture]
    [Category("sample")]
    public class CustomTest
    {
        [Test]
        public void SampleTest_WithStringData_Should_Pass()
        {
            // Arrange
            const string expectedAnswer = "50\n";
            using (var input = new MemoryStream(Encoding.UTF8.GetBytes("10\n")))
            using (var output = new MemoryStream())
            {
                // Act
                Program.Solve(input, output);
                var result = Encoding.UTF8.GetString(output.ToArray());

                // Assert
                Assert.That(result, Is.EqualTo(expectedAnswer));
            }
        }
    }
}
