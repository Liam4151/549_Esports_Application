using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Data_Manager
{
    // Class generates connection to database when requested by application 
    public static class SQLHelper
    {
        public static SqlConnection CreateSQLConnection(string name)
        {
            // After application requests connection string details, creates connection object for SQL 
            return new SqlConnection(GetConnectionString(name));
        }
        private static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
