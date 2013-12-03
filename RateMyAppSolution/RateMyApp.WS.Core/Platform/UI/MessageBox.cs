using RateMyApp.Core.Abstractions.UI;
using RateMyApp.Core.Rate;
using RateMyApp.Core.Resources;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace RateMyApp.WS.Core.Platform.UI
{
    public class MessageBox : IMessageBox
    {
        string PackageFamilyName = null;

        public MessageBox(string packageFamilyName) 
        {
            PackageFamilyName = packageFamilyName;
        }
        async public Task<MessageBoxResult> Show()
        {
            MessageBoxResult result = MessageBoxResult.Cancel;
            MessageDialog dialog = new MessageDialog(languagePack != null ? languagePack.RateMyAppHeader : AppResources.RateMyAppHeader);

            dialog.Commands.Add(new UICommand( languagePack != null ? languagePack.RateMyAppYesText : AppResources.RateMyAppYesText,
                new UICommandInvokedHandler(async cmd =>
                {
                    result = MessageBoxResult.Yes;
                    await MessageBox.LaunchReviewTask(PackageFamilyName);
                })));

            dialog.Commands.Add(new UICommand(languagePack != null ? languagePack.RateMyAppThridOptionText : AppResources.RateMyAppThridOptionText,
                new UICommandInvokedHandler(async cmd =>
                {
                    var mailto = new Uri(string.Format("mailto:?to={0}", languagePack != null ? languagePack.RateMyAppFeedbackEmail : AppResources.RateMyAppFeedbackEmail));
                    result = MessageBoxResult.ThirdOption;
                    await Windows.System.Launcher.LaunchUriAsync(mailto);
                })));

            dialog.Commands.Add(new UICommand(languagePack != null ? languagePack.RateMyAppCancelText :  AppResources.RateMyAppCancelText,
                new UICommandInvokedHandler(cmd =>
                {
                    result = MessageBoxResult.No;

                })));
            dialog.CancelCommandIndex = 0;
            dialog.DefaultCommandIndex = 1;

            await dialog.ShowAsync();
            return result;
        }

        async private static Task LaunchReviewTask(string PackageFamilyName)
        {
            
            await Windows.System.Launcher.LaunchUriAsync(new Uri(
                           string.Format("ms-windows-store:REVIEW?PFN={0}", PackageFamilyName)));
        }

        RateControlLanguagePack languagePack = null;
        public void SetLanguagePack(RateMyApp.Core.Rate.RateControlLanguagePack languagePack)
        {
            this.languagePack = languagePack;
        }

    }
}
