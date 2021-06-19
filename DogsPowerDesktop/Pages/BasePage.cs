using DogsPowerDesktop.Library;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DogsPowerDesktop
{
    /// <summary>
    /// Base page for all pages to gain base fynctionality
    /// </summary>
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        #region Private Member

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private VM _viewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The animation the play when the page is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation the play when the page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8f;

        /// <summary>
        /// A flag to indicate if this page should animate out on load.
        /// Useful for when we are moving the page to another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        public VM ViewModel
        {
            get => _viewModel;
            set
            {
                // If nothing has changed, return
                if (_viewModel == value)
                    return;

                // Update the value
                _viewModel = value;

                // Set the data context for this page
                DataContext = _viewModel;
            }
        }

        #endregion

        #region Constructor

        public BasePage()
        {
            // Don't bother animating in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // If we are animating in, hide to begin with
            if (PageLoadAnimation != PageAnimation.None)
            {
                Visibility = Visibility.Collapsed;
            }

            // Listen out for the page loading
            Loaded += BasePage_LoadedAsync;

            // Create a default view model
            ViewModel = new VM();
        }

        #endregion

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Animation Load / Unload

        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // If we are setup to animate out on load
            if (ShouldAnimateOut)
                // Animate out the page
                await AnimateOutAsync();
            // Otherwise...

            // Animate the page in
            await AnimateInAsync();
        }

        /// <summary>
        /// Animates in this page
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            // Make sure we have something to do
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:

                    // Start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);

                    break;
            }
        }

        /// <summary>
        /// Animates the page out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync()
        {
            // Make sure we have something to do
            if (PageUnloadAnimation == PageAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:

                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideSeconds);

                    break;
            }
        }

        #endregion
    }
}
