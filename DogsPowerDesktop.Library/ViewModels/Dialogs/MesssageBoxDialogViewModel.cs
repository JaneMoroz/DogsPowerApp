using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// Details for a message box dialog
    /// </summary>
    public class MessageBoxDialogViewModel : BaseViewModel
    {
        /// <summary>
        /// The title of the dialog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The message to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The text to use for the 'Yes/Ok' button
        /// </summary>
        public string OkText { get; set; }

        /// <summary>
        /// The text to use for the No/Cancel/Back' button
        /// </summary>
        public string NotOkText { get; set; }
    }
}
