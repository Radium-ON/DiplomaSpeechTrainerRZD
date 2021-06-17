

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Windows.UI.Xaml.Controls;
using SpeechTrainer.Core.Interfaces;

namespace SpeechTrainer.UWP.User.Results.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Results : Page, IPageHeader
    {
        public Results()
        {
            InitializeComponent();
        }

        #region Implementation of IPageHeader

        public string NavTitile => "Обзор результатов";

        #endregion
    }
}
