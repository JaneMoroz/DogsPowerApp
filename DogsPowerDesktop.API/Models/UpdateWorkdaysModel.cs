using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.API
{
    public class UpdateWorkdaysModel
    {
        public string GroomerId { get; set; }
        public List<string> GroomerWorkdays { get; set; }
    }
}
