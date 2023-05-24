using Flashcards.Helpers;
using Flashcards.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards.Controller;

internal class DbAccess
{
    private static readonly string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    //Data Source = (localdb)\FlashcardsServer;Initial Catalog = FlashCardsDB
    public static List<FlashCardDto> GetCardsWithoutAnswer(string stackId)//pass stack id
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

    public static List<FlashCard> GetCardsWithAnswers(string stackId)
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

    public static bool DisplayAllFlashcards(string stackId)//TODO close connection
    {
        bool cardsExist = true;
        string displayFlashcardsString;
        using (var connection = new SqlConnection(connectionString))
        {
            if (string.IsNullOrEmpty(stackId))
            {
                displayFlashcardsString =
                    $@"SELECT * FROM Flashcards
                       ORDER BY ID";
            }
            else
            {
                displayFlashcardsString =// may need to do a join here // add a foreign key stackid into session id that links to Stacks.ID
                    $@"SELECT * FROM Flashcards
                       WHERE StacksID = {stackId}";
            }

            using (SqlCommand displayFlashcards = new SqlCommand(displayFlashcardsString, connection))
            {
                connection.Open();
                var reader = displayFlashcards.ExecuteReader();
                var tableData = new List<FlashCard>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new FlashCard
                            {
                                Id = reader.GetInt32(0),
                                Front = reader.GetString(1),
                                Back = reader.GetString(2),
                                StacksId = reader.GetInt32(3),
                                StackName = reader.GetString(4),
                            });
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo cards found.");
                    Helper.ContinueMessage();
                    cardsExist = false;
                }
                Helper.DisplayData(tableData);
            }
        }
        return cardsExist;
    }

    public static void DisplayAllStacks(string stackId)
    {
        string displayStacksString;
        using (var connection = new SqlConnection(connectionString)) 
        {
            if (string.IsNullOrEmpty(stackId))
            {
                displayStacksString =
                    $@"SELECT * FROM Stacks
                       ORDER BY ID";
            }
            else
            {
                displayStacksString =
                    $@"SELECT ID, StackName
                       FROM Stacks
                       WHERE ID = '{stackId}'";
            }

            using (var displayStacks = new SqlCommand(displayStacksString, connection))
            {
                connection.Open();
                var reader = displayStacks.ExecuteReader();
                var tableData = new List<Stack>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new Stack
                            {
                                Id = reader.GetInt32(0),
                                StackName = reader.GetString(1),
                            });
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo stacks found.");
                }
                Helper.DisplayData(tableData);
            }
        }
    }

    public static void ViewSessions(string stacksId)// MAY NEED TO CHANGE SESSION STRING SO ITS ACCESSIBLE TO SQLCommand
    {
        string displaySessionsString;

        if (string.IsNullOrEmpty(stacksId))
        {
            displaySessionsString =
                $@"SELECT Date, Score, StacksID
                   FROM StudySessions";
        }
        else
        {
            displaySessionsString =// may need to do a join here // add a foreign key stackid into session id that links to Stacks.ID
                $@"SELECT Date, Score, StacksID, StackName
                   FROM StudySessions
                   WHERE StacksID = {stacksId}";
        }

        using (var connection = new SqlConnection(connectionString))
        {
            using (var displayStacks = new SqlCommand(displaySessionsString, connection))
            {
                connection.Open();
                var reader = displayStacks.ExecuteReader();
                var tableData = new List<StudySession>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new StudySession
                            {
                                Date = reader.GetDateTime(0),
                                Score = reader.GetInt32(1),
                                StackId = reader.GetInt32(2),
                                StackName = reader.GetString(3),
                            });
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo study sessions found.");
                }
                Helper.DisplayData(tableData);
            }
        }
    }

    internal static string RetrieveStackName(string stackId)
    {
        string retrieveStackNameString;

        using (var connection = new SqlConnection(connectionString))
        {

            //for loop through the select and increment id by one each time
            retrieveStackNameString =
                $@"SELECT StackName, ID
                   FROM Stacks
                   WHERE ID = {stackId}";

            using (var namesFromStack = new SqlCommand(retrieveStackNameString, connection))
            {
                connection.Open();
                var reader = namesFromStack.ExecuteReader();
                var tableData = new List<FlashCard>();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new FlashCard
                            {
                                StackName = reader.GetString(0),
                            });
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nStackName does not exist");
                }
                return tableData[0].StackName;
            }
        }
    }
}
