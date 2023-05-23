using Flashcards.Controller;
namespace Flashcards.Models;

internal class StudySession
{

    public DateTime Date { get; set; }

    public int Score { get; set; }

    public int StackId { get; set; }

    public string? StackName { get; set; }

    internal static StudySession CreateStudySession(DateTime date, int score, int stackId)
    {
        var session = new StudySession
        {
            Date = date,
            Score = score,
            StackId = stackId,
            StackName = DbAccess.RetrieveStackName(stackId.ToString())
        };
        return session;
    }
}