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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kidd_Esports_v3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTeamDetails_Click(object sender, RoutedEventArgs e)
        {
            TeamDetails Window = new TeamDetails();
            Window.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            Events Window = new Events();
            Window.ShowDialog();
        }

        private void btnGamesPlayed_Click(object sender, RoutedEventArgs e)
        {
            GamesPlayed Window = new GamesPlayed(); 
            Window.ShowDialog();
        }

        private void btnTeamResults_Click(object sender, RoutedEventArgs e)
        {
            TeamResults Window = new TeamResults();
            Window.ShowDialog();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            Reports Window = new Reports();
            Window.ShowDialog();
        }
    }
}
