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
    /// Interaction logic for Events.xaml
    /// </summary>
    public partial class Events : Window
    {
        // Creates AccessData object which interacts with SQL
        AccessData data = new AccessData();
        // Creates list for event entries
        List<EventsModel> listEvents = new List<EventsModel>();
        // New events model created for new database entries
        EventsModel Eventscurrent = new EventsModel();
        // Checks and decides whether data entry is new or updated
        bool newEntryIs = true;

        // Constructor opens and loads new events window and loads data grid entries
        public Events()
        {
            InitializeComponent();
            DataGridUpdate();
        }
        // Retrieves Events based on Id
        private int RetrieveSelectedIdFromDataGrid()
        {
            EventsModel selected = (EventsModel)Eventsdgv.SelectedItem;
            int id = selected.Id;
            return id;
        }

        // Retrieves Events and displays in data grid
        private void Eventsdgv_ChangedSelection(object sender, SelectionChangedEventArgs e)
        {
            if (Eventsdgv.SelectedIndex < 0)
            {
                return;
            }

            int id = RetrieveSelectedIdFromDataGrid();
            Eventscurrent = data.RetrieveEventsById(id);

            // Retrieves and displays event fields in data grid model
            txtId.Text = Eventscurrent.Id.ToString();
            txtEventName.Text = Eventscurrent.EventName;
            txtEventLocation.Text = Eventscurrent.EventLocation;
            txtEventDate.Text = Eventscurrent.EventDate;
            newEntryIs = false;
        }
        // Clears all textfields in stackpanel UI
        private void ClearEntry()
        {
            txtId.Text = "";
            txtEventName.Text = "";
            txtEventLocation.Text = "";
            txtEventDate.Text = "";
            newEntryIs = true;
        }
        // Updates data grid with new records. 
        private void DataGridUpdate()
        {
            listEvents = data.RetrieveEvents();
            Eventsdgv.ItemsSource = listEvents;
            Eventsdgv.Items.Refresh();
        }
        // Saves new entry when save button is pressed
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Current event details are read and put into events model. 
            Eventscurrent.EventName = txtEventName.Text;
            Eventscurrent.EventLocation = txtEventLocation.Text;
            Eventscurrent.EventDate = txtEventDate.Text;

            if (newEntryIs)
            {
                // Adds events record to database in new entry.
                data.EventsAdd(Eventscurrent);
            }
            else
            {

                data.EventsUpdate(Eventscurrent);
            }

            // Clears textfields 
            ClearEntry();
            // Updates data grid model with new saved entry
            DataGridUpdate();
        }
        // Clears textfields when new button is clicked
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearEntry();
        }
        // Deletes selected entry
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Eventsdgv.SelectedIndex < 0)
            {
                return;
            }
            // Selects entry from the model and data grid by Id
            EventsModel selected = (EventsModel)Eventsdgv.SelectedItem;
            int id = selected.Id;
            // Presents message to user, giving choice to delete entry
            MessageBoxResult result = MessageBox.Show("Delete entry?", "Confirm",
            MessageBoxButton.YesNo);
            // If user confirms, delete the record, clear entry fields and update the data grid
            if (result == MessageBoxResult.Yes)
            {
                data.RemoveEvents(id);
                ClearEntry();
                DataGridUpdate();
            }
        }
        // Closes events window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
   
