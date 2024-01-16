using Data_Manager;
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
using Data_Manager.Models;
using System.Xml.Linq;

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for GamesPlayed.xaml
    /// </summary>
    public partial class GamesPlayed : Window
    {
        // Creates AccessData object which interacts with SQL.
        AccessData data = new AccessData();
        // Creates list for GamesPlayed to retrieve from database
        List<GamesPlayedView> listGamesPlayed = new List<GamesPlayedView>();
        // Creates model for new or updated database entries
        GamesPlayedModel gamesPlayed = new GamesPlayedModel();
        // Creates object that refers to different save types
        SaveTypes saveType = SaveTypes.SaveNew;

        // List created for GameType that stores retrieved database entries
        List<GameTypeModel> listGameType = new List<GameTypeModel>();

        // Main constructor that loads window and data grid
        public GamesPlayed()
        {
            InitializeComponent();
            DataGridUpdate();
            ComboBoxSetup();
        }

        private void dgvGamesPlayed_ChangedSelection(object sender, SelectionChangedEventArgs e)
        {
            // Selected row in data grid is checked and returns index of first item
            if (GamesPlayeddgv.SelectedIndex < 0)
            {
                return;
            }

            // Games Played Id/primary key is retrieved from list
            int id = listGamesPlayed[GamesPlayeddgv.SelectedIndex].Id;
            // Retrieves GamesPlayed data from database via Id
            gamesPlayed = data.RetrieveGamesPlayedNameById(id);

            // Retrieves GamesPlayed data and stores in datagrid
            txtId.Text = gamesPlayed.Id.ToString();
            txtGameName.Text = gamesPlayed.GameName;
            cboGameType.SelectedValue = gamesPlayed.GameTypeId;
            saveType = SaveTypes.SaveUpdate;
        }

        private void ClearEntry()
        {
            txtId.Text = "";
            txtGameName.Text = "";
            cboGameType.SelectedIndex = -1;
            saveType = SaveTypes.SaveNew;
        }

        private void DataGridUpdate()
        {
            // Requests all records from database for GamesPlayed
            listGamesPlayed = data.RetrieveGamesPlayed();
            // GamesPlayed list/records given to datagrid
            GamesPlayeddgv.ItemsSource = listGamesPlayed;
            // Refreshes data grid
            GamesPlayeddgv.Items.Refresh();
        }

        private void ComboBoxSetup()
        {
            listGameType = data.RetrieveGameType();
            cboGameType.ItemsSource = listGameType;

            cboGameType.DisplayMemberPath = "GameTypeName";
            cboGameType.SelectedValuePath = "Id";
        }

        // Checks that fields are filled correctly. 
        public bool CheckFieldsAreFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtGameName.Text))
            {
                return false;
            }
            if (cboGameType.SelectedIndex < 0)
            {
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFieldsAreFilledCorrectly() == false)
            {
                MessageBox.Show("Please fill all fields correctly");
                return;
            }

            gamesPlayed.GameName = txtGameName.Text;
            gamesPlayed.GameTypeId = (int)cboGameType.SelectedValue;

            switch (saveType)
            {
                case SaveTypes.SaveNew:
                    data.GamesPlayedAdd(gamesPlayed);
                    break;
                case SaveTypes.SaveUpdate:
                    data.GamesPlayedNameUpdate(gamesPlayed);
                    break;
                default:
                    return;
            }

            DataGridUpdate();
            ClearEntry();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearEntry();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GamesPlayeddgv.SelectedIndex < 0)
            {
                return;
            }


            GamesPlayedView selected = (GamesPlayedView)GamesPlayeddgv.SelectedItem;

            int id = selected.Id;

            MessageBoxResult result = MessageBox.Show("Delete this record?", "Confirmation",
                                      MessageBoxButton.YesNo);

            // Displays yes message box if result is yes. 
            if (result == MessageBoxResult.Yes)
            {
                data.RemoveGamesPlayed(id);
                ClearEntry();
                DataGridUpdate();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cboGameType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
