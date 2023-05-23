using Flashcards.Helpers;
using Flashcards.Models;
using System.Configuration;
using System.Data.SqlClient;
namespace Flashcards.Controller;

internal class DbManager
{
    private static readonly string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    //create database first
    public static void CreateTables()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var createStacksString =
                $@"IF NOT EXISTS (SELECT * FROM sysobjects
                WHERE name='Stacks' and xtype='U')
                    CREATE TABLE Stacks(
	                ID INT PRIMARY KEY IDENTITY NOT NULL,
	                StackName NVARCHAR(20) NOT NULL UNIQUE)";

            using (SqlCommand createStacksTable = new SqlCommand(createStacksString, connection))
            {
                createStacksTable.ExecuteNonQuery();
            } 
            
            var createFlashcardsString =
                $@"IF NOT EXISTS (SELECT * FROM sysobjects
                WHERE name='Flashcards' and xtype='U')
                    CREATE TABLE Flashcards(
                    ID INT PRIMARY KEY IDENTITY NOT NULL,
                    Front NVARCHAR(25) NOT NULL,
                    Back NVARCHAR(25) NOT NULL,
                    StacksID INT FOREIGN KEY REFERENCES Stacks(ID),
                    StackName nvarchar(25) NOT NULL)";

            using (SqlCommand createFlashcardsTable = new SqlCommand(createFlashcardsString, connection))
            {
                createFlashcardsTable.ExecuteNonQuery();
            }
            
            var createStudySessionsString =
                $@"IF NOT EXISTS (SELECT * FROM sysobjects
                WHERE name='StudySessions' and xtype='U')
                    CREATE TABLE StudySessions(
	                Id INT PRIMARY KEY IDENTITY NOT NULL,
                    StackName NVARCHAR(25) NOT NULL,
	                Date DATETIME NOT NULL,
	                Score INT NOT NULL,
                    StacksID INT FOREIGN KEY REFERENCES Stacks(ID))";
            using (SqlCommand createStudySessionsTable = new SqlCommand(createStudySessionsString, connection))
            {
                createStudySessionsTable.ExecuteNonQuery();
            }
        }
    }

    internal static void InsertFlashcards()
    {
        var stackId = Helper.GetStackId("Enter the Id of the Stack you want to add to " +
                                        "or 0 to return to the Main Menu: ");
        var userInput = Helper.GetFlashcardValues();
        var stackName = DbAccess.RetrieveStackName(stackId);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var insertStackString =
                $@"INSERT INTO Flashcards (Front, Back, StacksID, StackName)
                   VALUES ('{userInput.Item1}', '{userInput.Item2}', {stackId}, '{stackName}')";

            using (SqlCommand insertStackCommand = new SqlCommand(insertStackString, connection))
            {
                connection.Open();
                insertStackCommand.ExecuteNonQuery();
            }
        }
    }

    internal static void UpdateFlashcard()
    {
        var stackId = Helper.GetStackId("Enter the Id of the Stack you want to update a " +
                                        "flashcard in or 0 to return to the Main Menu: ");
        var cardExist = DbAccess.DisplayAllFlashcards(stackId);

        if (cardExist)
        {
            Console.WriteLine("Enter the Id of the flashcard you want to update: ");
            var flashCardId = Console.ReadLine();
            var userInput = Helper.GetFlashcardValues();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var insertStackString =
                    $@"UPDATE Flashcards
                   SET  Front = '{userInput.Item1}', Back = '{userInput.Item2}', StacksID = {stackId}
                   WHERE ID = {flashCardId}";

                using (SqlCommand insertStackCommand = new SqlCommand(insertStackString, connection))
                {
                    connection.Open();
                    insertStackCommand.ExecuteNonQuery();
                }
            }
        }
        Console.Clear();
    }
    internal static void DeleteFlashcard()
    {
        var stackId = Helper.GetStackId("Enter the Id of the Stack you want to delete a " +
                                        "flashcard from or 0 to return to the Main Menu: ");
        var cardExists = DbAccess.DisplayAllFlashcards(stackId);

        if (cardExists)
        {
            Console.WriteLine("Enter the Id of the flashcard you want to delete: ");
            var flashCardId = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var deleteFlashcardString =
                    $@"DELETE FROM Flashcards
                   WHERE ID = {flashCardId}";

                using (SqlCommand deleteFlashcardCommand = new SqlCommand(deleteFlashcardString, connection))
                {
                    connection.Open();
                    deleteFlashcardCommand.ExecuteNonQuery();
                }
            }
        }
    }

    internal static void InsertStack()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            Console.WriteLine("Enter the name for the new stack: ");
            var userInput = Console.ReadLine().Trim().ToLower();
            var insertStackString =
                $@"INSERT INTO Stacks (StackName)
                   VALUES ('{userInput}')";

            using (SqlCommand insertStackCommand = new SqlCommand(insertStackString, connection))
            {
                try
                {
                    connection.Open();
                    insertStackCommand.ExecuteNonQuery();
                }
                catch
                {
                    Console.Write("You entered a Stack name that already exists. ");
                    Helper.ContinueMessage();
                }
            }
        }
    }

    internal static void DeleteStack()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var userInput = "";
            var stackId = "";

            stackId = Helper.GetStackId("Enter the Id of the stack you want to delete or 0 to return to the Main Menu: ");
            Helper.ConfirmStackDelete(stackId);

            var deleteFlashcardsFromStack =
                $@"DELETE FROM Flashcards
                   WHERE StacksID = {stackId}";

            using (SqlCommand deleteFlashcardsCommand = new SqlCommand(deleteFlashcardsFromStack, connection))
            {
                deleteFlashcardsCommand.ExecuteNonQuery();
            }

            var deleteStackString =
                $@"DELETE FROM Stacks
                   WHERE ID = {stackId}";

            using (SqlCommand deleteStackCommand = new SqlCommand(deleteStackString, connection))
            {
                var doesExist = deleteStackCommand.ExecuteNonQuery();
                if ( doesExist != 1)
                {
                    Console.WriteLine("No such stack exists. ");
                    Helper.ContinueMessage();
                }
            }
        }
    }

    internal static void InsertSession(StudySession session)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var stackName = DbAccess.RetrieveStackName(session.StackId.ToString());

            var insertSessionString =
                $@"INSERT INTO StudySessions (Date, Score, StacksID, StackName)
                   VALUES ('{session.Date}', {session.Score}, {session.StackId}, '{stackName}')";

            using (SqlCommand insertSessionCommand = new SqlCommand(insertSessionString, connection))
            {
                connection.Open();
                insertSessionCommand.ExecuteNonQuery();
            } 
        }
    }
}
