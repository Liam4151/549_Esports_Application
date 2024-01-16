using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Data_Manager.Models;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Security.AccessControl;

namespace Data_Manager
{
    public class DatabaseBuilder : AccessData
    {
        public void DatabaseCreation()
        {
            // Creates SQL connection to database using SQL helper class
            SqlConnection connection = SQLHelper.CreateSQLConnection("Default");
            try
            {
                // Re-writes connection string to connect to server
                string connString = $"Data Source={connection.DataSource}; Integrated Security = True";

                // Query checks for database name and new database is created if name cannot be found
                string query = $"IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = '{connection.Database}') " +
                               $"CREATE DATABASE {connection.Database}";

                // Opens the connection string if it is already closed
                using (connection = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (connection.State == System.Data.ConnectionState.Closed)
                        {
                            // Opens database connection
                            connection.Open();
                        }
                        // Uses sql command to execute query and return database rows
                        command.ExecuteNonQuery();
                        // Closes database connection
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        public bool CheckTablesExist()
        {
            // Creates SQL connection and configures it to database. 
            var connection = SQLHelper.CreateSQLConnection("Default");
            // Query that counts tables made by user existing in database
            string query = $"SELECT COUNT(*) FROM {connection.Database}.INFORMATION_SCHEMA.TABLES " +
                           $"WHERE TABLE_TYPE = 'BASE TABLE'";

            try
            {
                // Uses created connection string and disposes of it. 
                using (connection)
                {
                    // Executes above query that counts tables
                    int count = connection.QuerySingle<int>(query);
                    // If tables exist, (count is greater than 0), return true
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        // If count is 0 and no tables exist, return false
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Table creation in database 
        public void TableCreation(string name, string structure)
        {
            // Query creates tables with parameters in database
            string query = $"CREATE TABLE {name} ({structure})";
            try
            {
                // Establishes SQL connection to database
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        public void CreateDatabaseTables()
        {
            CreateTeamDetailsTable();
            CreateEventsTable();
            CreateGameTypeTable();
            CreateGamesPlayedTable();
            CreateSelectResultTypeTable();
            CreateTeamResultsTable();
        }

        // Creates Team Details Table 
        private void CreateTeamDetailsTable()
        {
            string name = "TeamDetails";
            // Table structure detailsl such as name, data type, nullability. 
            string structure = "Id int IDENTITY(1,1) PRIMARY KEY, " +
                               "TeamName VARCHAR(50) NOT NULL, " +
                               "PrimaryContact VARCHAR(50) NOT NULL, " +
                               "ContactPhone VARCHAR(50) NOT NULL, " +
                               "ContactEmail VARCHAR(50) NOT NULL, " +
                               "CompetitionPoints VARCHAR(50) NOT NULL";
            TableCreation(name, structure);
        }

        // Creates Events Table 
        private void CreateEventsTable()
        {
            // Table name. 
            string name = "Events";
            // Table structure detailsl such as name, data type, nullability. 
            string structure = "Id int IDENTITY(1,1) PRIMARY KEY, " +
                               "EventName VARCHAR(50) NOT NULL, " +
                               "EventLocation VARCHAR(50) NOT NULL, " +
                               "EventDate VARCHAR(50) NOT NULL ";
            TableCreation(name, structure);
        }
        // Creates Games Type Table 
        private void CreateGameTypeTable()
        {
            string name = "GameType";
            string structure = "Id int IDENTITY(1,1) PRIMARY KEY, " +
                               "GameTypeName VARCHAR(50) NOT NULL ";
            TableCreation(name, structure);
        }

        // Creates GamesPlayed Table 
        private void CreateGamesPlayedTable()
        {
            // Table name. 
            string name = "GamesPlayed";
            string structure = "Id int IDENTITY(1,1) PRIMARY KEY, " +
                               "GameName VARCHAR(50) NOT NULL, " +
                               "GameTypeId int NOT NULL " +
                               "FOREIGN KEY (GameTypeId) REFERENCES GameType(Id)";
            TableCreation(name, structure);
        }

        // Creates SelectResultType Table 
        private void CreateSelectResultTypeTable()
        {
            string name = "SelectResult";
            string structure = "Id int IDENTITY(1,1) PRIMARY KEY, " +
                               "ResultType VARCHAR(50) NOT NULL ";
            TableCreation(name, structure);
        }

        // Creates Team Results Table 
        private void CreateTeamResultsTable()
        {
            string name = "TeamResults";
            string structure = "Id int IDENTITY(1,1) PRIMARY KEY, " +
                               "EventHeldId int NOT NULL, " +
                               "GamePlayedId int NOT NULL, " +
                               "TeamId int NOT NULL, " +
                               "OpposingTeamId int NOT NULL, " +
                               "ResultId int NOT NULL " +
                               "FOREIGN KEY (EventHeldId) REFERENCES Events(Id), " +
                               "FOREIGN KEY (GamePlayedId) REFERENCES GamesPlayed(Id), " +
                               "FOREIGN KEY (TeamId) REFERENCES TeamDetails(Id), " +
                               "FOREIGN KEY (OpposingTeamId) REFERENCES TeamDetails(Id), " +
                               "FOREIGN KEY (ResultId) REFERENCES SelectResult(Id)";
            TableCreation(name, structure);
        }

        // Populates tables through methods

        public void PopulateTables()
        {
            TeamDetailsTablePopulate();
            EventsTablePopulate();
            GameTypeTablePopulate();
            GamesPlayedTablePopulate();
            SelectResultTablePopulate();
            TeamResultsTablePopulate();
        }

        // Populates the table for Team Details 
        private void TeamDetailsTablePopulate()
        {
            // Populates team details data into list  
            List<TeamDetailsModel> teamDetails = new List<TeamDetailsModel>();
            teamDetails.Add(new TeamDetailsModel("Pros", "Johnny", "1212004342", "johnny@outlook.com", "50"));
            teamDetails.Add(new TeamDetailsModel("Team X", "Dave", "1022102154", "Dave@gmail.com", "40"));
            teamDetails.Add(new TeamDetailsModel("Easy as", "Dan", "1024829042", "Dan@outlook.com", "30"));
            teamDetails.Add(new TeamDetailsModel("Hunters", "Jeremy", "8430421920", "Jez@gmail.com", "10"));
            teamDetails.Add(new TeamDetailsModel("TAFE ESPORTS", "Marcus", "219320421", "marcus@gmail.com", "90"));

            // Adds team details entry item
            foreach (var item in teamDetails)
            {
                TeamDetailsAdd(item);
            }
        }

        // Populate the table for Events
        private void EventsTablePopulate()
        {
            // Populates events data into list 
            List<EventsModel> events = new List<EventsModel>();
            events.Add(new EventsModel("2023 Finals", "South Bank", "29/03/2023"));
            events.Add(new EventsModel("Esports Playoffs", "Bracken Ridge", "23/02/2023"));
            events.Add(new EventsModel("GamesCon", "Brisbane City", "05/03/2023"));
            events.Add(new EventsModel("Project Esports", "Aspley", "20/02/2023"));

            foreach (var item in events)
            {
                // Adds events entries to database
                EventsAdd(item);
            }
        }

        // Populates table for GameType
        private void GameTypeTablePopulate()
        {
            // Adds Game Types to list 
            List<GameTypeModel> gameType = new List<GameTypeModel>();
            gameType.Add(new GameTypeModel("Solo"));
            gameType.Add(new GameTypeModel("Team"));

            foreach (var item in gameType)
            {
                // Adds list to database
                GameTypeAdd(item);
            }
        }

        // Populates the table for GamesPlayed 
        private void GamesPlayedTablePopulate()
        {
            // Adds each entry to list 
            List<GamesPlayedModel> gamesPlayed = new List<GamesPlayedModel>();
            gamesPlayed.Add(new GamesPlayedModel("Free For All", 1));
            gamesPlayed.Add(new GamesPlayedModel("TDM", 2));
            gamesPlayed.Add(new GamesPlayedModel("SnD", 2));
            gamesPlayed.Add(new GamesPlayedModel("Battle Royale", 1));
            gamesPlayed.Add(new GamesPlayedModel("Air Battle", 2));

            foreach (var item in gamesPlayed)
            {
                // Adds to GamesPlayed model
                GamesPlayedAdd(item);
            }
        }


        // Populates the table for TeamResults
        private void TeamResultsTablePopulate()
        {
            // Adds Results to list 
            List<TeamResultsModel> tResults = new List<TeamResultsModel>();
            tResults.Add(new TeamResultsModel(1, 1, 2, 1, 3));
            tResults.Add(new TeamResultsModel(2, 3, 2, 1, 1));
            tResults.Add(new TeamResultsModel(1, 2, 3, 4, 3));
            tResults.Add(new TeamResultsModel(3, 2, 1, 2, 2));

            foreach (var item in tResults)
            {
                TeamResultsAdd(item);
            }
        }

        // Populates the table for SelectResults
        private void SelectResultTablePopulate()
        {
            List<SelectResultType> selectResult = new List<SelectResultType>();
            selectResult.Add(new SelectResultType("Win"));
            selectResult.Add(new SelectResultType("Draw"));
            selectResult.Add(new SelectResultType("Loss"));
            foreach (var item in selectResult)
            {
                SelectResultAdd(item);
            }
        }
    }
}

