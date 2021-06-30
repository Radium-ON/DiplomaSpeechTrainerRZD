using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.DependencyInjection;
using SpeechTrainer.Core.Interfaces;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.UWP.PlatformTools;
using SpeechTrainer.UWP.Training.History.Operation;
using SpeechTrainer.UWP.Training.History.View;
using SpeechTrainer.UWP.Training.HistoryDetails.Operation;
using SpeechTrainer.UWP.Training.HistoryDetails.View;
using SpeechTrainer.UWP.Training.TrainingRun.Operation;
using SpeechTrainer.UWP.Training.TrainingRun.View;
using SpeechTrainer.UWP.Training.TrainingStart.Operation;
using SpeechTrainer.UWP.Training.TrainingStart.View;
using SpeechTrainer.UWP.User.Results.Operation;
using SpeechTrainer.UWP.User.Results.View;
using SpeechTrainer.UWP.User.SignIn.Operation;
using SpeechTrainer.UWP.User.SignIn.View;
using SpeechTrainer.UWP.User.SignUp.Operation;
using SpeechTrainer.UWP.User.SignUp.View;

namespace SpeechTrainer.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(SignIn), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<TrainingService, TrainingService>();
            services.AddSingleton<AnalyticsService, AnalyticsService>();
            services.AddSingleton<IPlayer, MediaPlayerFoundation>();
            services.AddSingleton<ISpeechToText<RecognitionResult>, SpeechService>();
            services.AddSingleton<IPrivacySettings, PrivacySettingsEnabler>();

            services.AddSingleton<GetStudentsOption, GetStudentsOption>();
            services.AddSingleton<SignUpOptions, SignUpOptions>();
            services.AddSingleton<TrainingHistoryOptions, TrainingHistoryOptions>();
            services.AddSingleton<TrainingDetailsOptions, TrainingDetailsOptions>();
            services.AddSingleton<TrainingStartOptions, TrainingStartOptions>();
            services.AddSingleton<TrainingRunOptions, TrainingRunOptions>();
            services.AddSingleton<ResultsOptions, ResultsOptions>();

            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<HistoryViewModel>();
            services.AddTransient<HistoryDetailsViewModel>();
            services.AddTransient<TrainingStartViewModel>();
            services.AddTransient<TrainingRunViewModel>();
            services.AddTransient<ResultsViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
