using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.API
{
    public class UploadProfilePictureModel
    {
        public string GroomerId { get; set; }
        public byte[] Picture { get; set; }
    }
}
