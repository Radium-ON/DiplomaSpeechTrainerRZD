

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SpeechTrainer.Core.Interfaces;
using SpeechTrainer.Core.Utills;

namespace SpeechTrainer.UWP.Training.TrainingStart.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingStart : Page, IPageHeader
    {
        public TrainingStartViewModel ViewModel { get; private set; }

        public TrainingStart()
        {
            this.InitializeComponent();
            ViewModel = new TrainingStartViewModel();
        }

        private void TrainingRun_OnClick(object sender, RoutedEventArgs e)
        {
            Extensions.FindParent<Frame>(Frame).Navigate(typeof(TrainingRun.View.TrainingRun));
        }

        #region Implementation of IPageHeader

        public string NavTitile => "Отработка регламента переговоров";

        #endregion
    }
}
