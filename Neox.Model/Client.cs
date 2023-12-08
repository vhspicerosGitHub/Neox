using System.Diagnostics.CodeAnalysis;

namespace Neox.Model;

[ExcludeFromCodeCoverage]
public class Client
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }
}