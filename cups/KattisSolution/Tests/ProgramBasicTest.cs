using System.IO;
using System.Text;
using NUnit.Framework;

namespace KattisSolution.Tests
{
   // [Ignore("Uncoment this only when there's no data files")]
    [TestFixture]
    [Category("sample")]
    public class ProgramBasicTest
    {
        [Test]
        public void Solver_Should_ReturnIntMulitpliedBy5_When_SampleImplementation()
        {
            // Arrange
            using (var stdin = new MemoryStream(Encoding.UTF8.GetBytes("2\nred 123\n10 blue ")))
            using (var stdout = new MemoryStream())
            {
                // Act
                Program.Solve(stdin, stdout);
                var result = Encoding.UTF8.GetString(stdout.ToArray());

                // Assert
                Assert.That(result, Is.EqualTo("blue\nred"));
            }
        }
    }
}
