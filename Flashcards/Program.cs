using Flashcards.Controller;
using System.Diagnostics;
using System.Security.Principal;
using System.Xml;

namespace Flashcards;

internal class Program
{
    static void Main(string[] args)
    {
        DbAccess.DisplayAllFlashcards();
        //var controller = new DbController();
        ////controller.CreateDb();
        //controller.CreateTables();
    }
}