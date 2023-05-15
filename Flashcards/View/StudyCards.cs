using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Helpers;
using Flashcards.Models;
using System.Threading.Tasks.Sources;

namespace Flashcards.View;

internal class StudyCards
{
    public static void Study()
    {
        //display first row of that stack without the Back of card
        //ask user for answer
        //check if answer was correct
        //prompt user to press enter to go to next card


        var stackIdInput = Helper.GetStackId("Enter the Id of the stack you would like to study");
        //show first card where stack id = stackIdInput
        Console.Clear();
        var stackOfCardsWithoutAnswers = DbAccess.CardsFromStackWithoutAnswer(stackIdInput);
        var stackOfCardsWithAnswers = DbAccess.CardsFromStackWithAnswer(stackIdInput);
        var score = 0;// use 

        var cardsToStudy = new List<FlashCardDto>();

        for (int i = 0; i < stackOfCardsWithoutAnswers.Count; i++) // make a count variable
        {
            cardsToStudy.Add(stackOfCardsWithoutAnswers[i]);
            
            ConsoleTableBuilder
                .From(cardsToStudy)// needs to change to index
                .WithTitle($"{stackOfCardsWithAnswers[0].StacksId}")
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
                Console.WriteLine($"Wrong! Correct answer was {stackOfCardsWithAnswers[i].Back}");

            cardsToStudy.Remove(cardsToStudy[0]);//removes card without answer from cards to study. now null
                                      
        }
    }
}
