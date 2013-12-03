using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WSMiniumTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            RateMyApp.WS.Core.RateMyAppControl control = new RateMyApp.WS.Core.RateMyAppControl();

            // auto language (english only, more languages to come later)
            control.Start(false,"8fb5ebf0-3ba8-4d68-9f2a-7154d8c0ac6a_70rq6xpyaxwzy",null);

            // debug = true + 'Debug build' => 
            // Review will apear after 10 sec. regardless of previous state 
            // Callback: this metod is called everytime the Control changes "review" state.
            // Exposure Pattern: in this sample the Rate control will be shown 
            // the first 6 times that app has been used for more than 10 sec. 
            // or until the review has been completed. 
            control.Start(true, "8fb5ebf0-........-yaxwzy", new RateMyApp.Core.Rate.RateControlLanguagePack
            {
                RateMyAppCancelText = "No",
                RateMyAppFeedbackEmail = "my-app@email.com",
                RateMyAppHeader = "Header",
                RateMyAppText = "text body",
                RateMyAppThridOptionText = "Send feedback by e-mail",
                RateMyAppYesText = "yes"
            }, null, new int[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
