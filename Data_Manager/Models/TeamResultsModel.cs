using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    public class TeamResultsModel
    {
        public TeamResultsModel() 
        {

        }

        public int Id { get; set; }
        public int EventHeldId { get; set; }
        public int GamePlayedId { get; set; }
        public int TeamId { get; set; }
        public int OpposingTeamId { get; set; }
        public int ResultId { get; set; }

        public TeamResultsModel(int eventHeldId, int gamePlayedId, int teamId, int opposingTeamId, int resultId)
        {
            EventHeldId = eventHeldId;
            GamePlayedId = gamePlayedId;
            TeamId = teamId;
            OpposingTeamId = opposingTeamId;
            ResultId = resultId;
        }
    }
}
