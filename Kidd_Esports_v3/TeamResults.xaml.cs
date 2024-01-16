using Data_Manager;
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

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for TeamResults.xaml
    /// </summary>
    public partial class TeamResults : Window
    {
        // Creates object that uses AccessData class to manage query/transactions in database
        AccessData data= new AccessData();  
        // List to hold Team Results records
        List<TeamResultsView> listTeamResults = new List<TeamResultsView>();
        public TeamResults()
        {
            InitializeComponent();
            DataGridUpdate();
        }
        private void DataGridUpdate()
        {
            // Retrieves database records. 
            listTeamResults = data.RetrieveTeamResults();
            // Datagrid list assigned. 
            TeamResultsdgv.ItemsSource = listTeamResults;
            //Refreshes display of results. 
            TeamResultsdgv.Items.Refresh();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            TeamResultsNew window = new TeamResultsNew();   

            if (window.ShowDialog() == true) 
            {
                DataGridUpdate();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TeamResultsdgv.SelectedIndex < 0)
            {
                return;
            }
            TeamResultsView selected = (TeamResultsView)TeamResultsdgv.SelectedItem;

            //TeamResultsModel selected = (TeamResultsModel)TeamResultsdgv.SelectedItem;
            int id = selected.Id;
            // Presents message to user, giving choice to delete entry
            MessageBoxResult result = MessageBox.Show("Delete entry?", "Confirm",
            MessageBoxButton.YesNo);
            // If user confirms, delete the record, clear entry fields and update the data grid
            if (result == MessageBoxResult.Yes)
            {
                data.RemoveTeamResults(id);
                //ClearEntry();
                DataGridUpdate();
            }
        }
    }
}
