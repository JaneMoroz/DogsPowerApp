using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.API
{
    public class AuthenticatedUserModel
    {
        public string Access_Token { get; set; }
        public string UserName { get; set; }
    }
}
