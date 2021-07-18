using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.API
{
    public class WeightPriceDurationModel
    {
        public string WeightName { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
