using Data_Manager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Constructor created
        public App() 
        {
            // Database builder object is created
            DatabaseBuilder builder = new DatabaseBuilder();
            // Executes method to creat database
            builder.DatabaseCreation();
            // Checks if database tables exist
            if (builder.CheckTablesExist() == false) 
            {
                // If no tables exist, method creates them 
                builder.CreateDatabaseTables();
                // If no tables exist, method populates them
                builder.PopulateTables();
            }
        }
    }
}
