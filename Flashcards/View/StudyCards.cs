using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Helpers;
using Flashcards.Models;
namespace Flashcards.View;

internal class StudyCards
{
    public static void Study()
    {
        var score = 0;
        var stackIdInput = Helper.GetStackId("Enter the Id of the stack you would like to study");
        Console.Clear();

        var cardsWithoutAnswers = DbAccess.GetCardsWithoutAnswer(stackIdInput);
        var cardsWithAnswers = DbAccess.GetCardsWithAnswers(stackIdInput);

        var cardsToStudy = new List<FlashCardDto>();

        for (int i = 0; i < cardsWithoutAnswers.Count; i++) 
        {
            cardsToStudy.Add(cardsWithoutAnswers[i]);

            ConsoleTableBuilder
                .From(cardsToStudy)
                .WithTitle($"{cardsWithAnswers[0].StackName}")
                .ExportAndWriteLine();

            Console.WriteLine("Type your answer and press enter: ");
            var answer = Console.ReadLine();

            Console.Clear();
            if (answer.Trim().ToLower() == cardsWithAnswers[i].Back)
            {
                Console.WriteLine("Correct!");
                score++;
            }
            else
                Console.WriteLine($"Wrong! Correct answer was \"{cardsWithAnswers[i].Back}\"");

            cardsToStudy.Remove(cardsToStudy[0]);
        }
        try
        {
            var session = StudySession.CreateStudySession(score, cardsWithAnswers[0].StacksId);
            DbManager.InsertSession(session);
        }
        catch (ArgumentOutOfRangeException) 
        {
            Console.WriteLine("There are no cards to study in this stack.");
        }
    }
}
