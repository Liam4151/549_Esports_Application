using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Data_Manager.Models;
using System.Security.AccessControl;
using System.Data;

namespace Data_Manager
{
    public class AccessData
    {
        #region TeamDetails
        // Retrieves Team Details list saved in database
        public List<TeamDetailsModel> RetrieveTeamDetails()
        {
            try
            {
                // Uses SQL Helper connection to create sql connection to database that will end with using statement
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Selects data from database through SQL query
                    string query = "SELECT * FROM TeamDetails";
                    // Returns model data to list
                    return connection.Query<TeamDetailsModel>(query).ToList();
                }
            }
            catch (Exception e)
            {

                return new List<TeamDetailsModel>();
            }
        }
        // Retrives Team Details by entry's Id through SQL connection
        public TeamDetailsModel RetrieveTeamDetailsById(int id)
        {
            try
            {
                // Uses SQL Helper connection to create sql connection to database that will end with the using statement
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string query = $"SELECT * FROM TeamDetails WHERE Id = {id}";
                    return connection.QuerySingle<TeamDetailsModel>(query);
                }
            }
            catch (Exception e)
            {

                return TeamDetailsModel();
            }
        }
        // *** STOPPING ERRORS BUT ALSO MAY STOP FUNCTIONALITY CONERN WITH THIS ***
        private TeamDetailsModel TeamDetailsModel()
        {
            throw new NotImplementedException();
        }

        // Adds Team Details entry to the database which will be executed through the save button
        public void TeamDetailsAdd(TeamDetailsModel teamDetails)
        {
            try
            {
                // Uses SQL Helper connection to create sql connection to database that will end with the using statement
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Query to be executed (Within sql server) to add entered data in textfields to database table within database
                    string query = "INSERT INTO TeamDetails (TeamName, PrimaryContact, ContactPhone, ContactEmail, CompetitionPoints) " +
                                   "VALUES (@TeamName, @PrimaryContact, @ContactPhone, @ContactEmail, @CompetitionPoints)";
                    // Executes the query and data model through connection
                    connection.Execute(query, teamDetails);

                }
            }
            catch (Exception e)
            {

            }
        }

        public void RemoveTeamDetails(int id)
        {
            try
            {
                // Uses SQL Helper connection to create sql connection to database that will end with the using statement
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Query is sent to SQL SERVER to delete records from the Team Details table in database based on Id
                    string query = $"DELETE FROM TeamDetails WHERE Id = {id}";
                    // String query is executed through connection 
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        // Updates the current entry within the Team Details table in database
        public void UpdateTeamDetails(TeamDetailsModel teamDetails)
        {
            try
            {
                // Uses SQL Helper connection to create sql connection to database that will end with the using statement
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Query to be sent to sql server to be executed 
                    string query = "UPDATE TeamDetails SET " +
                                   "TeamName = @TeamName, PrimaryContact = @PrimaryContact, " +
                                   "ContactPhone = @ContactPhone, ContactEmail = @ContactEmail " +
                                   "WHERE Id = @Id";
                    // Executes above query in database for team details 
                    connection.Execute(query, teamDetails);
                }
            }
            catch (Exception)
            {

            }
        }
        // Updates Team Details through method overloading
        public void UpdateTeamDetailsById(int id, SqlConnection connection, SqlTransaction transaction, int points)
        {
            try
            {
                // String query sent to SQL which updates int fields in record
                string query = "UPDATE TeamDetails SET " +
                               $"CompetitionPoints = CompetitionPoints + {points} " +
                               $"WHERE Id = {id}";
                // Executes query in SQL and allows for null through a SQL transaction
                connection.Execute(query, null, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Events
        // Retrieves events entries through SQL and displays in data grid model
        public List<EventsModel> RetrieveEvents()
        {
            try
            {
                // Sets an SQL connection for query
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Query selects data from events
                    string query = "SELECT * FROM Events";
                    return connection.Query<EventsModel>(query).ToList();
                }
            }
            catch (Exception e)
            {

                return new List<EventsModel>();
            }
        }
        // Retrieves Events entries through Id's through SQL query
        public EventsModel RetrieveEventsById(int id)
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string query = $"SELECT * FROM Events WHERE Id = {id}";
                    return connection.QuerySingle<EventsModel>(query);
                }
            }
            catch (Exception e)
            {

                return new EventsModel();
            }
        }

        // Adds events entries to database through SQL connection query
        public void EventsAdd(EventsModel events)
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO Events (EventName, EventLocation, EventDate) " +
                                   "VALUES (@EventName, @EventLocation, @EventDate)";
                    connection.Execute(query, events);

                }
            }
            catch (Exception e)
            {

            }
        }

        // Removes an events entry from the database through SQL query 
        public void RemoveEvents(int id)
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string query = $"DELETE FROM Events WHERE Id = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Updates events entry through SQL query
        public void EventsUpdate(EventsModel events)
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string query = "UPDATE Events SET " +
                                   "EventName = @EventName, EventLocation = @EventLocation, " +
                                   "EventDate = @EventDate " +
                                   "WHERE Id = @Id";
                    connection.Execute(query, events);
                }
            }
            catch (Exception)
            {


            }
        }

        #endregion

        #region GamesPlayed

        // Requests GamesPlayed entries from database

        public List<GamesPlayedView> RetrieveGamesPlayed()
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string sql = "SELECT GamesPlayed.Id, GamesPlayed.GameName, " +
                                 "GameType.GameTypeName " +
                                 "FROM GameType INNER JOIN " +
                                 "GamesPlayed ON GameType.Id = GamesPlayed.GameTypeId";
                    return connection.Query<GamesPlayedView>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<GamesPlayedView>();
            }
        }
        // Retrieves Games Played entries by Id through Sql Server to display in datagrid 
        public GamesPlayedModel RetrieveGamesPlayedNameById(int id)
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string sql = $"SELECT * FROM GamesPlayed WHERE Id = {id}";
                    return connection.QuerySingle<GamesPlayedModel>(sql);
                }
            }
            catch (Exception e)
            {
                return new GamesPlayedModel();
            }
        }

        // Adds GamesPlayed entry to database through SQL connection and query
        public void GamesPlayedAdd(GamesPlayedModel gamesPlayed)
        {
            try
            {
                // Uses Sql connection string to connect to database
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Adds GamesPlayed values to database through SQL query
                    string sql = "INSERT INTO GamesPlayed " +
                                 "(GameName, GameTypeId) " +
                                 "VALUES (@GameName, @GameTypeId) ";
                    // Executes the above query and saves to database
                    connection.Execute(sql, gamesPlayed);
                }
            }
            catch (Exception e)
            {

            }
        }

        // Removes/deletes GamesPlayed entry in the database based on id
        public void RemoveGamesPlayed(int id)
        {
            try
            {
                // Uses SQL connection string to connect to database
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // SQL query that deletes GamesPlayed record based on Id 
                    string query = $"DELETE FROM GamesPlayed WHERE Id = {id}";
                    // Executes the query
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        // GamesPlayed entry is updated in database
        public void GamesPlayedNameUpdate(GamesPlayedModel gamesPlayed)
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // String query used in SQL that updates GamesPlayed values
                    string sql = "UPDATE GamesPlayed " + "SET GameName = @GameName, " + "GameTypeId = @GameTypeId " +
                        "WHERE Id = @Id";
                    // Executes sql query and updates database
                    connection.Execute(sql, gamesPlayed);
                }
            }
            catch (Exception e)
            {

            }
        }

        // Requests GameType and creates list. 
        public List<GameTypeModel> RetrieveGameType()
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    string sql = "SELECT * FROM GameType";
                    return connection.Query<GameTypeModel>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<GameTypeModel>();
            }
        }
        // Adds GameType to database. 
        public void GameTypeAdd(GameTypeModel gameType)
        {
            try
            {
                string query = "INSERT INTO GameType(GameTypeName) " +
                               "VALUES (@GameTypeName)";
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    connection.Execute(query, gameType);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region TeamResults
        // Retrieves Team Results in list
        public List<TeamResultsView> RetrieveTeamResults()
        {
            try
            {
                // Creates SQL connection using SQLHelper class
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Query for retrieving needed data in Sql
                    string query = "SELECT TeamResults.Id, eventJoin.EventName AS EventName, gameJoin.GameName AS GameName, " +
                                   "firstTeamJoin.TeamName AS Team, otherTeamJoin.TeamName AS OpposingTeam, " +
                                   "selectResultJoin.ResultType AS Result " +
                                   "FROM TeamResults " +
                                   "INNER JOIN " +
                                   "Events eventJoin ON eventJoin.Id = TeamResults.EventHeldId " +
                                   "INNER JOIN " +
                                   "GamesPlayed gameJoin ON gameJoin.Id = TeamResults.GamePlayedId " +
                                   "INNER JOIN " +
                                   "TeamDetails firstTeamJoin ON firstTeamJoin.Id = TeamResults.TeamId " +
                                   "INNER JOIN " +
                                   "TeamDetails otherTeamJoin ON otherTeamJoin.Id = TeamResults.OpposingTeamId " +
                                   "INNER JOIN " +
                                   "SelectResult selectResultJoin ON selectResultJoin.Id = TeamResults.ResultId";

                    // Returns connection and executes query
                    return connection.Query<TeamResultsView>(query).ToList();
                }
            }
            // Catches error and returns list to data grid
            catch (Exception e)
            {
                return new List<TeamResultsView>();
            }
        }

        // Takes Team Results data and adds to database in SQL 
        public void TeamResultsAdd(TeamResultsModel build)
        {
            try
            {
                // Query for adding team results data into sql table in SQL
                string query = "INSERT INTO TeamResults (EventHeldId, GamePlayedId, TeamId, OpposingTeamId, ResultId) " +
                               "VALUES (@EventHeldId, @GamePlayedId, @TeamId, @OpposingTeamId, @ResultId)";
                // Establishes SQL connection through SQLHelper class 
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Executes the query and adds to database
                    connection.Execute(query, build);
                }
            }
            // Catches error and stops application
            catch (Exception e)
            {

            }
        }
        //CHECK THIS MAY NOT WORK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public void RemoveTeamResults(int id)
        {
            try
            {
                // Uses SQL connection string to connect to database
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // SQL query that deletes GamesPlayed record based on Id 
                    string query = $"DELETE FROM TeamResults WHERE Id = {id}";
                    // Executes the query
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        // Creates a SQL Transaction that Manages Competition Points 
        public bool ManageTransactionCompetitionPoints(TeamResultsModel result)
        {
            
            using (var connection = SQLHelper.CreateSQLConnection("Default"))
            {
                // Checks the state of data connection
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    // If closed, this opens the connection 
                    connection.Open();
                }
                // Begins transaction query
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Query to execute transaction to manage teams points
                        string query = "INSERT INTO TeamResults (EventHeldId, GamePlayedId, TeamId, OpposingTeamId, ResultId) " +
                                       "VALUES (@EventHeldId, @GamePlayedId, @TeamId, @OpposingTeamId, @ResultId)";

                        // Executes the above query
                        connection.Execute(query, result, transaction);
                        if (result.ResultId == 1)
                        {
                            //Updates Team's competition points
                            UpdateTeamDetailsById(result.TeamId, connection, transaction, 2);
                        }
                        else if (result.ResultId == 3)
                        {
                            //Updates Team's competition points
                            UpdateTeamDetailsById(result.OpposingTeamId, connection, transaction, 2);
                        }
                        else
                        {
                            //Updates Team's competition points
                            UpdateTeamDetailsById(result.TeamId, connection, transaction, 1);
                            UpdateTeamDetailsById(result.OpposingTeamId, connection, transaction, 1);
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        // If error is caught, rollback transaction and undo changes
                        transaction.Rollback();
                        return false;
                    }
                }

            }
        }

        // Retrieves Result Type and put in list 
        public List<SelectResultType> RetrieveResultType()
        {
            try
            {
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Writes string query and sends to SQL
                    string query = "SELECT * FROM SelectResult ";
                    // Returns connection and retrieves in list
                    return connection.Query<SelectResultType>(query).ToList();
                }
            }
            // Catches error and returns original list 
            catch (Exception e)
            {

                return new List<SelectResultType>();
            }
        }
        // Adds a new Result entry
        public void SelectResultAdd(SelectResultType selectResult)
        {
            try
            {
                // Uses Sql connection to connect to database
                using (var connection = SQLHelper.CreateSQLConnection("Default"))
                {
                    // Sql query inserts reult type value into database
                    string sql = "INSERT INTO SelectResult " +
                                 "(ResultType) " +
                                 "VALUES (@ResultType) ";
                    // Executes query in sql through connection
                    connection.Execute(sql, selectResult);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}