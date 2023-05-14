
namespace Flashcards.Models;

internal class StudySessions
{
    public int Id { get; set; }

    public DateTime Date { get; set; }
    public int Score { get; set; }

    public int StackId { get; set; }

    public string? StackName { get; set; }
}

internal class StudySessionDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int Score { get; set; }

    public string? StackName { get; set; }  
}