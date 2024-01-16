using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    public class GamesPlayedModel
    {
        public GamesPlayedModel()
        {

        }

        public int Id { get; set; }
        public string GameName { get; set; }
        public int GameTypeId { get; set; }

        public GamesPlayedModel(string gameName, int gameTypeId)
        {
            GameName = gameName;
            GameTypeId = gameTypeId;
        }
    }
}

