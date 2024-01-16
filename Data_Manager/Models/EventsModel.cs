using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    // Events Model class
    public class EventsModel
    {
        public EventsModel()
        {

        }
        
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public string EventDate { get; set; }

        // Sets the fields for events (id, name, location, date)
        public EventsModel(string eventName, string eventLocation, string eventDate)
        {
            EventName = eventName;
            EventLocation = eventLocation;
            EventDate = eventDate;
        }
    }
}

