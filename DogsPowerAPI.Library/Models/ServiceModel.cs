using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class ServiceModel
    {
        public string ServiceName { get; set; }
        public List<WeightPriceDurationModel> Options { get; set; }
    }
}
