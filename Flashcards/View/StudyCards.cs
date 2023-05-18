using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Helpers;
using Flashcards.Models;
namespace Flashcards.View;

internal class StudyCards
{
    public static void Study()
    {

        var date = DateOnly.FromDateTime(DateTime.Now);
        var score = 0;

        var stackIdInput = Helper.GetStackId("Enter the Id of the stack you would like to study");
        Console.Clear();
        var stackOfCardsWithoutAnswers = DbAccess.CardsFromStackWithoutAnswer(stackIdInput);
        var stackOfCardsWithAnswers = DbAccess.CardsFromStackAllProperties(stackIdInput);

        var cardsToStudy = new List<FlashCardDto>();

        for (int i = 0; i < stackOfCardsWithoutAnswers.Count; i++) 
        {
            cardsToStudy.Add(stackOfCardsWithoutAnswers[i]);

            ConsoleTableBuilder
                .From(cardsToStudy)
                .WithTitle($"{stackOfCardsWithAnswers[0].StackName}")
                .ExportAndWriteLine();

            Console.WriteLine("Type your answer and press enter: ");
            var answer = Console.ReadLine();
            Console.Clear();
            if (answer.Trim().ToLower() == stackOfCardsWithAnswers[i].Back)
            {
                Console.WriteLine("Correct!");
                score++;
            }
            else
                Console.WriteLine($"Wrong! Correct answer was \"{stackOfCardsWithAnswers[i].Back}\"");

            cardsToStudy.Remove(cardsToStudy[0]);
        }
        var session = new StudySession
        {
            Date = date,
            Score = score,
            StackId = stackOfCardsWithAnswers[0].StacksId,
            StackName = stackOfCardsWithAnswers[0].StackName,
            
        };
        DbManager.InsertSession(session);
    }
}
