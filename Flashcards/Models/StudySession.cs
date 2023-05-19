
namespace Flashcards.Models;

internal class StudySession
{

    public DateTime Date { get; set; }

    public int Score { get; set; }

    public int StackId { get; set; }

    public string? StackName { get; set; }

    internal static StudySession CreateStudySession(DateTime date, int score, int StackId, string StackName)
    {
        var session = new StudySession
        {
            Date = date,
            Score = score,
            StackId = StackId,
            StackName = StackName,
        };
        return session;
    }
}

internal class StudySessionDto
{
    public DateTime Date { get; set; }

    public int Score { get; set; }

    public string? StackName { get; set; }
}
