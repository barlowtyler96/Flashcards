
namespace Flashcards.Models;

internal class StudySession
{

    public DateOnly Date { get; set; }
    public int Score { get; set; }

    public int StackId { get; set; }

    public string? StackName { get; set; }
}