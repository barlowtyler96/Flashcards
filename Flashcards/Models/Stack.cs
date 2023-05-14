
namespace Flashcards.Models;

internal class Stack
{
    public int Id { get; set; } //stackid in flashcards links to this

    public string? StackName { get; set; }
}

internal class StackDto
{
    public string? StackName { get; set; }  
}
