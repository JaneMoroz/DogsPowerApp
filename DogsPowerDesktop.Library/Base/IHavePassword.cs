using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// An interface for a class that can provide a secure password
    /// </summary>
    public interface IHavePassword
    {
        /// <summary>
        /// The secure password
        /// </summary>
        SecureString SecurePassword { get; }
    }
}
