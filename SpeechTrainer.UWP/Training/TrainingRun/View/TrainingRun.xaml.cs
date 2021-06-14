

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpeechTrainer.UWP.Shell.NavigationPage.View;

namespace SpeechTrainer.UWP.Training.TrainingRun.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingRun : Page
    {
        public TrainingRun()
        {
            InitializeComponent();
        }

        private async void NavigationPage_OnClick(object sender, RoutedEventArgs e)
        {
            var goToSignInDialog = new ContentDialog
            {
                Title = "Выход",
                Content = "Вы действительно хотите прервать попытку?\nРезультат не будет записан.",
                PrimaryButtonText = "Закончить попытку",
                SecondaryButtonText = "Остаюсь"
            };

            var result = await goToSignInDialog.ShowAsync();

            switch (result)
            {
                case ContentDialogResult.Primary:
                    Frame.Navigate(typeof(NavigationPage));
                    break;
                case ContentDialogResult.Secondary:
                    ;
                    break;
            }
        }
    }
}
