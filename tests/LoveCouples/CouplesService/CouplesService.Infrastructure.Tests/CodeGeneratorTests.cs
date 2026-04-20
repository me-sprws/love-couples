using CouplesService.Infrastructure.Configuration;
using CouplesService.Infrastructure.Services;

namespace CouplesService.Infrastructure.Tests;

using Xunit;

public sealed class CodeGeneratorTests
{
    const int DefaultCodeLength = 10;
    
    [Fact]
    public void GenerateCode_ShouldHaveCorrectLength()
    {
        // Arrange
        var config = new CodeGeneration(DefaultCodeLength, "ABCDEF");
        var generator = new CodeGenerator(config);

        // Act
        var result = generator.GenerateCode();

        // Assert
        Assert.Equal(10, result.Length);
    }

    [Fact]
    public void GenerateCode_ShouldContainOnlyAllowedSymbols()
    {
        // Arrange
        var symbols = "ABCDEF";
        var config = new CodeGeneration(DefaultCodeLength, symbols);
        var generator = new CodeGenerator(config);

        // Act
        var result = generator.GenerateCode();

        // Assert
        Assert.All(result, c =>
            Assert.Contains(c, symbols));
    }
}