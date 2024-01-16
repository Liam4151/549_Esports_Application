using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    public class GameTypeModel
    {
        public GameTypeModel() 
        { 

        }

        public int Id { get; set; } 
        public string GameTypeName { get; set; }    

        public GameTypeModel(string gameTypeName)
        {
            GameTypeName = gameTypeName;
        }
    }
}
