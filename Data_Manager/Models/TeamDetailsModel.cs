using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    // Data model for Team Details
    public class TeamDetailsModel
    {
        public TeamDetailsModel()
        {

        }

        public int Id { get; set; }
        public string TeamName { get; set; }
        public string PrimaryContact { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string CompetitionPoints { get; set; }

        // Sets team details fields for model
        public TeamDetailsModel(string teamName, string primaryContact, string contactPhone, string contactEmail, string competitionPoints)
        {
            TeamName = teamName;
            PrimaryContact = primaryContact;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            CompetitionPoints = competitionPoints;
        }
    }
}
