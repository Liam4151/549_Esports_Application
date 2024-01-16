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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data_Manager;
using Data_Manager.Models;

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for TeamDetails.xaml
    /// </summary>
    // Window class for Team Details GUI.

    public partial class TeamDetails : Window
    {
        // Creates database object to interact with SQL. 
        AccessData data = new AccessData();
        // Creates list for records in team details. 
        List<TeamDetailsModel> listTeamDetails = new List<TeamDetailsModel>();
        // New team details model created for new database entries. 
        TeamDetailsModel TeamDetailscurrent = new TeamDetailsModel();
        // Determines current entry is new or updated.
        bool newEntryIs = true;

        // Constructor that loads window and data grid. 
        public TeamDetails()
        {
            InitializeComponent();
            DataGridUpdate();
        }
        // Retrieves Team Details based on Id
        private int RetrieveSelectedIdFromDataGrid()
        {
            TeamDetailsModel selected = (TeamDetailsModel)TeamDetailsdgv.SelectedItem;
            int id = selected.Id;
            return id;
        }
        // Retrieves Team Details and displays in data grid
        private void TeamDetailsdgv_ChangedSelection(object sender, SelectionChangedEventArgs e)
        {
            if (TeamDetailsdgv.SelectedIndex < 0)
            {
                return;
            }
            int id = RetrieveSelectedIdFromDataGrid();
            TeamDetailscurrent = data.RetrieveTeamDetailsById(id);

            // Retrieves and displays team details in fields for editing. 
            txtId.Text = TeamDetailscurrent.Id.ToString();
            txtTeamName.Text = TeamDetailscurrent.TeamName;
            txtPrimaryContact.Text = TeamDetailscurrent.PrimaryContact;
            txtContactPhone.Text = TeamDetailscurrent.ContactPhone;
            txtContactEmail.Text = TeamDetailscurrent.ContactEmail;
            txtCompetitionPoints.Text = TeamDetailscurrent.CompetitionPoints;
            newEntryIs = false;
        }
        // Clears all textfields in stackpanel UI
        private void ClearEntry()
        {
            txtId.Text = "";
            txtTeamName.Text = "";
            txtPrimaryContact.Text = "";
            txtContactPhone.Text = "";
            txtContactEmail.Text = "";
            txtCompetitionPoints.Text = "";
            newEntryIs = true;
        }
        // Updates data grid model with new saved entry
        private void DataGridUpdate()
        {
            listTeamDetails = data.RetrieveTeamDetails();
            TeamDetailsdgv.ItemsSource= listTeamDetails;
            TeamDetailsdgv.Items.Refresh(); 
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Data is read from textfields and added
            TeamDetailscurrent.TeamName = txtTeamName.Text;
            TeamDetailscurrent.PrimaryContact = txtPrimaryContact.Text;
            TeamDetailscurrent.ContactPhone = txtContactPhone.Text;
            TeamDetailscurrent.ContactEmail = txtContactEmail.Text;
            TeamDetailscurrent.CompetitionPoints = txtCompetitionPoints.Text;
            // isNewEntry flag is checked and set for new or existing record. 
            if (newEntryIs)
            {
                data.TeamDetailsAdd(TeamDetailscurrent);
            }
            else
            {
                data.UpdateTeamDetails(TeamDetailscurrent);
            }
            // Clears all entries, sets back to true. 
            ClearEntry();
            // Refreshes and displays new data grid. 
            DataGridUpdate();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearEntry();
        }
        // Deletes team details entries
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TeamDetailsdgv.SelectedIndex < 0)
            {
                return;
            }
            // Selects entry from the model and data grid by Id
            TeamDetailsModel selected = (TeamDetailsModel) TeamDetailsdgv.SelectedItem;
            int id = selected.Id;
            // Presents message to user, giving choice to delete entry
            MessageBoxResult result = MessageBox.Show("Delete entry?", "Confirm",
            MessageBoxButton.YesNo);
            // If user confirms, delete the record, clear entry fields and update the data grid
            if (result == MessageBoxResult.Yes) 
            {
                data.RemoveTeamDetails(id);
                ClearEntry();
                DataGridUpdate();
            }
        }
        // Closes application 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
