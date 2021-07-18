using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class GroomerTimeOptions
    {
        public string GroomerId { get; set; }
        public int NumberOfAppointments { get; set; }
        public List<TimeSpan> AvailableTimeOptions { get; set; }
    }
}
