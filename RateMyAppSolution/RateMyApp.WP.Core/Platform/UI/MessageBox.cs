using RateMyApp.Core.Abstractions.UI;
using RateMyApp.Core.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.WP.Core.Platform.UI
{
    public class MessageBox : IMessageBox
    {
        public MessageBox()
        {

        }
        async public Task<MessageBoxResult> Show()
        {
            MessageBoxResult status = MessageBoxResult.Cancel;
            System.Windows.MessageBoxResult result = System.Windows.MessageBoxResult.None;
            if (languagePack == null)
            {
                result = System.Windows.MessageBox.Show(RateMyApp.Core.Resources.AppResources.RateMyAppText, RateMyApp.Core.Resources.AppResources.RateMyAppHeader, System.Windows.MessageBoxButton.OKCancel);
            }
            else
            {
                result = System.Windows.MessageBox.Show(languagePack.RateMyAppText, languagePack.RateMyAppHeader, System.Windows.MessageBoxButton.OKCancel);
            }


            if (result == System.Windows.MessageBoxResult.Cancel)
            {
                status = MessageBoxResult.Cancel;
            }
            else if (result == System.Windows.MessageBoxResult.No)
            {
                status = MessageBoxResult.No;

            }
            else if (result == System.Windows.MessageBoxResult.None)
            {
                status = MessageBoxResult.Cancel;

            }
            else if (result == System.Windows.MessageBoxResult.OK)
            {
                status = MessageBoxResult.Yes;
                Microsoft.Phone.Tasks.MarketplaceReviewTask task = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
                task.Show();
            }
            else if (result == System.Windows.MessageBoxResult.Yes)
            {
                status = MessageBoxResult.Yes;
                Microsoft.Phone.Tasks.MarketplaceReviewTask task = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
                task.Show();
            }
            // Hack
            var helper = await Task.Run(() => { return status; });
            return helper;
        }

        RateControlLanguagePack languagePack = null;
        public void SetLanguagePack(RateMyApp.Core.Rate.RateControlLanguagePack languagePack)
        {
            this.languagePack = languagePack;
        }
    }
}
