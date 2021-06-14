using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using SpeechTrainer.UWP.Training.History.View;
using SpeechTrainer.UWP.Training.HistoryDetails.View;
using SpeechTrainer.UWP.Training.TrainingStart.View;
using SpeechTrainer.UWP.User.Results.View;
using SpeechTrainer.UWP.User.SignIn.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeechTrainer.UWP.Shell.NavigationPage.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationPage : Page
    {
        private bool _alreadyStarted = false;
        public NavigationPage()
        {
            InitializeComponent();
        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (_alreadyStarted) return;
            _alreadyStarted = true;
            var item = args.SelectedItem as NavigationViewItem;
            var itemTag = (string)item?.Tag;
            switch (itemTag)
            {
                case "Training":
                    main_frame.Navigate(typeof(TrainingStart));
                    break;
                case "History":
                    main_frame.Navigate(typeof(History));
                    break;
                case "Results":
                    main_frame.Navigate(typeof(Results));
                    break;
            }
        }

        private async void NavigationViewFooterItem_TappedAsync(object sender, TappedRoutedEventArgs e)
        {
            var goToSignInDialog = new ContentDialog
            {
                Title = "Выход",
                Content = "Вы действительно хотите сменить пользователя?",
                PrimaryButtonText = "Да, выйти",
                SecondaryButtonText = "Не хочу"
            };

            var result = await goToSignInDialog.ShowAsync();

            switch (result)
            {
                case ContentDialogResult.Primary:
                    Frame.Navigate(typeof(SignIn));
                    break;
                case ContentDialogResult.Secondary:
                    e.Handled = true;
                    break;
            }
        }

        private void Navigation_view_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                main_frame.Navigate(typeof(Settings.View.Settings));
            }
            else
            {
                var item = args.InvokedItemContainer as NavigationViewItem;
                var itemTag = (string)item?.Tag;

                switch (itemTag)
                {
                    case "Training":
                        main_frame.Navigate(typeof(TrainingStart));
                        break;
                    case "History":
                        main_frame.Navigate(typeof(History));
                        break;
                    case "Results":
                        main_frame.Navigate(typeof(Results));
                        break;
                }

            }
        }
    }
}
