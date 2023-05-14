using Flashcards.Controller;
using Flashcards.Helpers;

namespace Flashcards.View;

internal class StudyCards
{
    public static void Study()
    {
        var stackIdInput = Helper.GetStackId();
        //show first card where stack id = stackIdInput
        var stackOfCards = DbAccess.GetCardsFromStack(stackIdInput);


    }
}
