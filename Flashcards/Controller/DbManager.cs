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
                    StacksID INT FOREIGN KEY REFERENCES Stacks(ID))";

            using (SqlCommand createFlashcardsTable = new SqlCommand(createFlashcardsString, connection))
            {
                createFlashcardsTable.ExecuteNonQuery();
            }
            
            var createStudySessionsString =
                $@"IF NOT EXISTS (SELECT * FROM sysobjects
                WHERE name='Flashcards' and xtype='U')
                    CREATE TABLE StudySessions(
	                Id INT PRIMARY KEY IDENTITY NOT NULL,
	                Date DATE NOT NULL,
	                Score INT NOT NULL,
	                StackName nvarchar(25) NOT NULL,
                    StacksID INT FOREIGN KEY REFERENCES Stacks(ID))";
            using (SqlCommand createStudySessionsTable = new SqlCommand(createStudySessionsString, connection))
            {
                createStudySessionsTable.ExecuteNonQuery();
            }
        }
    }

    internal static void InsertFlashcards()
    {
        //display all stacks
        //ask user to enter stack id and use that id as where 
    }
    internal static void UpdateFlashcards()
    {
        //display flashcards with input id
        //ask user to to input id of card they want to delete
    }
    internal static void DeleteFlashcards()
    {
        //display flashcards with input id
        //ask user to input  id of card they want to delete
    }
    internal static void InsertStack()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            Console.WriteLine("Enter the name for the new stack: ");
            var userInput = Console.ReadLine();
            var insertStackString =
                $@"INSERT INTO Stacks (StackName)
                   VALUES ('{userInput}')";

            using (SqlCommand insertStackCommand = new SqlCommand(insertStackString, connection))
            {
                connection.Open();
                insertStackCommand.ExecuteNonQuery();
            }
        }
    }
    internal static void DeleteStack()
    {
        Console.Clear();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var inputConfirmed = false;
            var userInput = "";
            DbAccess.DisplayAllStacks();
            while (inputConfirmed == false)
            {
                Console.WriteLine("Enter the Id of the stack you want to delete: ");
                userInput = Console.ReadLine();
                Console.WriteLine($"Are you sure you want to delete the following stack: {userInput}?\n" +
                                  $"(enter yes or no)");
                var confirmInput = Console.ReadLine().Trim().ToLower();
                if (confirmInput == "yes") //todoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
                {
                    inputConfirmed = true;
                }
            }
            var deleteStackString =
                $@"DELETE FROM Stacks
                   WHERE ID = {userInput}";

            using (SqlCommand deleteStackCommand = new SqlCommand(deleteStackString, connection))
            {
                deleteStackCommand.ExecuteNonQuery();
            }

            var deleteFlashcardsFromStack = 
                $@"DELETE FROM Flashcards
                   WHERE StacksID = {userInput}";
            
            using (SqlCommand deleteFlashcardsCommand = new SqlCommand(deleteFlashcardsFromStack, connection))
            {
                deleteFlashcardsCommand.ExecuteNonQuery();
            }
        }
    }

    internal static void InsertSession(StudySession session)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            var insertSessionString =
                $@"INSERT INTO StudySessions (Date, Score, StackName, StacksID)
                   VALUES ('{session.Date}', {session.Score}, '{session.StackName}', {session.StackId})";

            using (SqlCommand insertSessionCommand = new SqlCommand(insertSessionString, connection))
            {
                connection.Open();
                insertSessionCommand.ExecuteNonQuery();
            } 
        }
    }
}
