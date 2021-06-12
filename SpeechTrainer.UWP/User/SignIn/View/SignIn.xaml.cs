using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using SpeechTrainer.UWP.Shell.NavigationPage.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeechTrainer.UWP.User.SignIn.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn : Page
    {
        public SignInViewModel ViewModel => (SignInViewModel) DataContext;

        public SignIn()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<SignInViewModel>();
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp.View.SignUp));
        }

        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NavigationPage));
        }

        #region Overrides of Page

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        #endregion
    }
}
