using Data_Manager;
using Data_Manager.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        // List for combo box options created. 
        List<string> listReportTypes = new List<string> { "Team Details by Competition Points", "Team Results by Event", "Team Results by Team" };
        AccessData data = new AccessData();

        // Lists for each field. 
        List<TeamDetailsModel> teamDetailsListFull = new List<TeamDetailsModel>();
        List<TeamDetailsModel> teamDetailsListFiltered = new List<TeamDetailsModel>();
        List<TeamDetailsModel> teamDetailsListDisplay = new List<TeamDetailsModel>();

        // Results lists created. 
        List<TeamResultsView> resultListFull = new List<TeamResultsView>();
        List<TeamResultsView> resultListFiltered = new List<TeamResultsView>();
        List<TeamResultsView> resultListDisplay = new List<TeamResultsView>();
        public Reports()
        {
            InitializeComponent();
            cboReportType.ItemsSource = listReportTypes;
        }

        // Team details list displayed in report 
        private void TeamDetailsListDisplay(List<TeamDetailsModel> listActive)
        {
            teamDetailsListDisplay = listActive;
            Reportdgv.ItemsSource = teamDetailsListDisplay;
            Reportdgv.Items.Refresh();
        }
        // Active Result list displayed 
        private void TeamResultsListDisplay(List<TeamResultsView> listActive)
        {
            resultListDisplay = listActive;
            Reportdgv.ItemsSource = resultListDisplay;
            Reportdgv.Items.Refresh();
        }

        private void ComboBoxResult_ChangedSelection(object sender, SelectionChangedEventArgs e)
        {
            switch (cboReportType.SelectedIndex)
            {
                case 0:
                    teamDetailsListFull = data.RetrieveTeamDetails();
                    teamDetailsListFull = teamDetailsListFull.OrderByDescending(c => c.CompetitionPoints).ThenBy(c => c.TeamName).ToList();
                    TeamDetailsListDisplay(teamDetailsListFull);
                    break;

                case 1:
                    resultListFull = data.RetrieveTeamResults();
                    resultListFull = resultListFull.OrderBy(p => p.EventName).ToList();
                    TeamResultsListDisplay(resultListFull);
                    break;

                case 2:
                    resultListFull = data.RetrieveTeamResults();
                    resultListFull = resultListFull.OrderBy(p => p.Team).ToList();
                    TeamResultsListDisplay(resultListFull);
                    break;

                default:
                    return;
            }
            txtSearch.Text = "";
        }

        // Search method for team detail records.
        private void TeamDetailsSearch()
        {
            if (String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                TeamDetailsListDisplay(teamDetailsListFull);
                return;
            }
            // Records in team details list found through linq.
            teamDetailsListFiltered = teamDetailsListFull.Where(c => c.CompetitionPoints.ToUpper().Contains(txtSearch.Text.ToUpper()) ||
                                                            c.TeamName.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();

            TeamDetailsListDisplay(teamDetailsListFiltered);
        }

        // Searches Team Results.
        public void TeamResultSearch()
        {
            if (String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                TeamResultsListDisplay(resultListFull);
                return;
            }
            // Filters records in full result list with search field sequence located.
            resultListFiltered = resultListFull.Where(p => p.EventName.ToUpper().Contains(txtSearch.Text.ToUpper()) ||
                                                            p.Result.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();


            TeamResultsListDisplay(resultListFiltered);
        }

        private void TextChanged_txtSearch(object sender, TextChangedEventArgs e)
        {
            if (cboReportType.SelectedIndex == -1)
            {
                return;
            }

            if (cboReportType.SelectedIndex == 0)
            {
                TeamDetailsSearch();
            }
            else if (cboReportType.SelectedIndex > 0)
            {
                TeamResultSearch();
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (cboReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select one of report types before export");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Comma Delimited Values|*.csv|" +
                            "Plain Text File|*.txt";
            // Sets the directory the applicaiton will on first instance open to. 
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.OverwritePrompt = true;

            if (dialog.ShowDialog() == true)
            {
                if (cboReportType.SelectedIndex == 0)
                {
                    // Opens steam reader to retrieve file path and name. 
                    using (StreamWriter writer = new StreamWriter(dialog.FileName))
                    {
                        foreach (var team in teamDetailsListDisplay)
                        {
                            writer.WriteLine($"{team.Id},{team.TeamName},{team.PrimaryContact}," +
                            $"{team.ContactPhone},{team.ContactEmail},{team.CompetitionPoints}");
                        }
                    }
                }
                else if (cboReportType.SelectedIndex > 0)
                {
                    using (StreamWriter writer = new StreamWriter(dialog.FileName))
                    {
                        foreach (var teamResults in resultListDisplay)
                        {
                            writer.WriteLine($"{teamResults.Id},{teamResults.EventName},{teamResults.GameName}," +
                            $"{teamResults.Team},{teamResults.OpposingTeam},{teamResults.Result}");
                        }
                    }
                }
            }
        }

        private void Reportdgv_ChangedSelection(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
