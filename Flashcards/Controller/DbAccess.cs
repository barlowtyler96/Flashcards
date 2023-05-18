using ConsoleTableExt;
using Flashcards.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Flashcards.Controller;

internal class DbAccess
{
    private static readonly string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    //Data Source = (localdb)\FlashcardsServer;Initial Catalog = FlashCardsDB
    public static List<FlashCardDto> CardsFromStackWithoutAnswer(string stackId)//pass stack id
    {
        using (var connection = new SqlConnection(connectionString))
        {
            
            //for loop through the select and increment id by one each time
            var cardsFromStackString =
                $@"SELECT Flashcards.Front, Flashcards.Back
                 FROM Stacks
                 JOIN Flashcards
                 ON Stacks.ID=Flashcards.StacksId
                 WHERE Stacks.ID = {stackId}";

            using (var readCardsCommand = new SqlCommand(cardsFromStackString, connection))
            {
                connection.Open();
                var reader = readCardsCommand.ExecuteReader();
                var tableData = new List<FlashCardDto>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new FlashCardDto
                            {
                                Front = reader.GetString(0),
                                Back = "Answer"
                            });
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo cards found.");
                }
                return tableData;
            } 
        }
    }

    public static List<FlashCard> CardsFromStackAllProperties(string stackId)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            
            //for loop through the select and increment id by one each time
            var cardsFromStackString =
                $@"SELECT Stacks.StackName, Stacks.ID, Flashcards.Back
                 FROM Stacks
                 JOIN Flashcards
                 ON Stacks.ID=Flashcards.StacksID
                 WHERE Stacks.ID = {stackId}";

            using (var cardsFromStack = new SqlCommand(cardsFromStackString, connection))
            {
                connection.Open();
                var reader = cardsFromStack.ExecuteReader();
                var tableData = new List<FlashCard>();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new FlashCard
                            {
                                StackName = reader.GetString(0),
                                StacksId = reader.GetInt32(1),
                                Back = reader.GetString(2)
                            }); ;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo cards found.");
                }
                return tableData;
            }
        }
    }

    public static void DisplayAllFlashcards()//TODO close connection
    {
        using (var connection = new SqlConnection(connectionString))
        {
            
            var displayStacksString =
                $@"SELECT * FROM Flashcards
                ORDER BY Id";

            using (SqlCommand displayStacks = new SqlCommand(displayStacksString, connection))
            {
                connection.Open();
                var reader = displayStacks.ExecuteReader();
                var dataTable = new DataTable();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo cards found.");
                }
                ConsoleTableBuilder
                            .From(dataTable)
                            .ExportAndWriteLine();
            }
        }
    }

    public static void DisplayAllStacks()//TODO close connection
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var displayStacksString =
                $@"SELECT * FROM Stacks
                ORDER BY Id";

            using (var displayStacks = new SqlCommand(displayStacksString, connection))
            {
                connection.Open();
                var reader = displayStacks.ExecuteReader();
                var dataTable = new DataTable();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo stacks found.");
                }
                ConsoleTableBuilder
                            .From(dataTable)
                            .ExportAndWriteLine();
            }
        }
    }

    public static void ViewSessions(string sessionId)// MAY NEED TO CHANGE SESSION STRING SO ITS ACCESSIBLE TO SQLCommand
    {
        string displaySessionsString;
        
        if (string.IsNullOrEmpty(sessionId))
        {
            displaySessionsString =
                $@"SELECT Date, Score, StackName
                   FROM StudySessions";
        }
        else
        {
            displaySessionsString =// may need to do a join here // add a foreign key stackid into session id that links to Stacks.ID
                $@"SELECT Date, Score, StackName
                   FROM StudySessions
                   WHERE ";
        }
        
        using (var connection = new SqlConnection(connectionString))
        {
            using (var displayStacks = new SqlCommand(displaySessionsString, connection))
            {
                connection.Open();
                var reader = displayStacks.ExecuteReader();
                var dataTable = new DataTable();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo stacks found.");
                }
                ConsoleTableBuilder
                            .From(dataTable)
                            .ExportAndWriteLine();
            }
        }
    }
}
