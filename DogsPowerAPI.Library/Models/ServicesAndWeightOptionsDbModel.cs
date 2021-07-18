using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class ServicesAndWeightOptionsDbModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
        public string WeightName { get; set; }
    }
}
