using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    public class TeamResultsView
    {
        public TeamResultsView() 
        { 

        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public string GameName { get; set; }
        public string Team { get; set; }
        public string OpposingTeam { get; set; }
        public string Result { get; set; }

        public TeamResultsView(string eventName, string gameName, string team, string opposingTeam, string result)
        {
            EventName = eventName;
            GameName = gameName;
            Team = team;
            OpposingTeam = opposingTeam;
            Result = result;
        }
    }
}
