using CouplesService.Domain.Services;
using CouplesService.Infrastructure.Configuration;

namespace CouplesService.Infrastructure.Services;

public sealed class CodeGenerator(CodeGeneration generation) : ICodeGenerator
{
    public string GenerateCode()
    {
        return new(Enumerable
            .Repeat(generation.Symbols, generation.CodeLength)
            .Select(s => s[Random.Shared.Next(s.Length)])
            .ToArray());
    }
}