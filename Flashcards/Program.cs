using Flashcards.Controller;
using Flashcards.View;
using System.Diagnostics;
using System.Security.Principal;
using System.Xml;

namespace Flashcards;

internal class Program
{
    static void Main(string[] args)
    {
        StudyCards.Study();
        //DbAccess.DisplayAllFlashcards();
        //var controller = new DbController();
        ////controller.CreateDb();
        //controller.CreateTables();
    }
}