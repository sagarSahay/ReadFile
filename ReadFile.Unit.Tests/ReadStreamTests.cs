namespace ReadFile.Unit.Tests
{
    using System.IO;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class ReadStreamTests
    {

        [Fact]
        public void Read_WithProperPath_ShouldReadFile()
        {
            // Arrange
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            var filePath = configuration.GetSection("FilePath").Value;
            var SUT = new ReadStream("abcd");

            // Act 

            var result = SUT.ReadLine();

            // Assert

            result.Should().NotBe(string.Empty);
        }
    }
}
