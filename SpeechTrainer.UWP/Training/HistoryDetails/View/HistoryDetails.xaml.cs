

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using SpeechTrainer.Core.Interfaces;

namespace SpeechTrainer.UWP.Training.HistoryDetails.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryDetails : Page, IPageHeader
    {
        public HistoryDetailsViewModel ViewModel => (HistoryDetailsViewModel)DataContext;


        public HistoryDetails()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<HistoryDetailsViewModel>();
        }

        #region Implementation of IPageHeader

        public string NavTitile => "Ответы";

        #endregion

        #region Overrides of Page

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.IsActive = false;
            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.IsActive = true;
            base.OnNavigatedTo(e);
        }

        #endregion
    }
}
