using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Helpers;
using Flashcards.Models;

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
        var stackOfCards = DbAccess.GetCardsFromStack(stackIdInput);



        //var cards = new List<FlashCardDto>();
        //foreach (var card in stackOfCards)
        //{
        //    cards.Add(card);
        //    ConsoleTableBuilder
        //     .From(cards)
        //     .ExportAndWriteLine();
        //}
    }
}
