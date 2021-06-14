

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using SpeechTrainer.Core.Interfaces;
using SpeechTrainer.Core.Utills;

namespace SpeechTrainer.UWP.Training.TrainingStart.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingStart : Page, IPageHeader
    {
        public TrainingStartViewModel ViewModel => (TrainingStartViewModel)DataContext;

        public TrainingStart()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<TrainingStartViewModel>();
        }

        private void TrainingRun_OnClick(object sender, RoutedEventArgs e)
        {
            Extensions.FindParent<Frame>(Frame).Navigate(typeof(TrainingRun.View.TrainingRun));
        }

        #region Overrides of Page

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.IsActive = true;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.IsActive = false;
            base.OnNavigatingFrom(e);
        }

        #endregion

        #region Implementation of IPageHeader

        public string NavTitile => "Отработка регламента переговоров";

        #endregion
    }
}
