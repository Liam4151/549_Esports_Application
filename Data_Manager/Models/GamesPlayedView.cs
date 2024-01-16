using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    public class GamesPlayedView
    {
        public GamesPlayedView() 
        {

        }
        public int Id { get; set; } 
        public string GameName { get; set; }        
        public string GameTypeName { get; set; }

        public GamesPlayedView(string gameName, string gameTypeName)
        {
            GameName = gameName;
            GameTypeName = gameTypeName;
        }
    }
}
