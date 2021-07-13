using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class UploadProfilePictureModel
    {
        public string GroomerId { get; set; }
        public byte[] Picture { get; set; }
    }
}
