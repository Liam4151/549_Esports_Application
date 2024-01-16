using Data_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data_Manager;
using Data_Manager.Models;
using System.Diagnostics;

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for TeamResultsNew.xaml
    /// </summary>
    public partial class TeamResultsNew : Window
    {
        // Creates object that uses AccessData class to manage query/transactions in database
        AccessData data = new AccessData();
        // Lists created for comboboxes 
        List<TeamDetailsModel> teamDetailsList = new List<TeamDetailsModel>();
        List<EventsModel> eventsList = new List<EventsModel>();
        List<GamesPlayedView> gamesPlayedList = new List<GamesPlayedView>();
        List<SelectResultType> resultTypeList = new List<SelectResultType>();

        public TeamResultsNew()
        {
            // Loads New Team Results window 
            InitializeComponent();

            // Combo boxes populated with table lists
            teamDetailsList = data.RetrieveTeamDetails();
            eventsList = data.RetrieveEvents();
            gamesPlayedList = data.RetrieveGamesPlayed();
            resultTypeList = data.RetrieveResultType();

            ComboBoxSetup();
        }

        // Method sets up combo boxes. Displays names and ids. 
        public void ComboBoxSetup()
        {
            cboOppsoingTeam.ItemsSource = teamDetailsList;
            cboOppsoingTeam.DisplayMemberPath = "TeamName";
            cboOppsoingTeam.SelectedValuePath = "Id";

            cboTeam.ItemsSource = teamDetailsList;
            cboTeam.DisplayMemberPath = "TeamName";
            cboTeam.SelectedValuePath = "Id";

            cboEventHeld.ItemsSource = eventsList;
            cboEventHeld.DisplayMemberPath = "EventName";
            cboEventHeld.SelectedValuePath = "Id";

            cboGamePlayed.ItemsSource = gamesPlayedList;
            cboGamePlayed.DisplayMemberPath = "GameName";
            cboGamePlayed.SelectedValuePath = "Id";

            cboResult.ItemsSource = resultTypeList;
            cboResult.DisplayMemberPath = "ResultType";
            cboResult.SelectedValuePath = "Id";
        }

        // Selected option is checked from combo boxes. Returns false value if no entry is selected.
        private bool CheckFieldsAreFilledCorrectly()
        {
            if (cboEventHeld.SelectedIndex < 0)
            {
                return false;
            }
            if (cboGamePlayed.SelectedIndex < 0)
            {
                return false;
            }
            if (cboTeam.SelectedIndex < 0)
            {
                return false;
            }
            if (cboOppsoingTeam.SelectedIndex < 0)
            {
                return false;
            }
            if (cboResult.SelectedIndex < 0)
            {
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // If fields are not filled in combo boxes, returns false. 
            if (CheckFieldsAreFilledCorrectly() == false)
            {
                return;
            }

            // Creates and assigns new build objects to values from comboboxes. 
            TeamResultsModel build = new TeamResultsModel();
            build.TeamId = (int)cboTeam.SelectedValue;
            build.OpposingTeamId = (int)cboOppsoingTeam.SelectedValue;
            build.EventHeldId = (int)cboEventHeld.SelectedValue;
            build.GamePlayedId = (int)cboGamePlayed.SelectedValue;
            build.ResultId = (int)cboResult.SelectedValue;

            // New build added to database, returns true and closes window for true response. 
            if (data.ManageTransactionCompetitionPoints(build) == true)
            {
                // Dialog response set for the window and closes. Returns value back to show dialog method. 
                DialogResult = true;
            }
            else
            {
                // User is warned that it did not function as expected. 
                MessageBox.Show("Something went wrong. Please try again.");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= false;    
        }
    }
}

