using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpeechTrainer.UWP.Shell.NavigationPage.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeechTrainer.UWP.User.SignUp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
        }

        private void ToSignIn_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn.View.SignIn));
        }

        private void ToNavigationPage_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NavigationPage));
        }
    }
}
