using RateMyApp.Core.Rate;
using RateMyApp.Core.SharedSource.Storage;
using RateMyApp.WP.Core.Platform.UI;
using System;

namespace RateMyApp.WP.Core
{
    public class RateMyAppControl
    {
        public bool Start(bool enableDebugMode, RateControlLanguagePack languagePack)
        {
            return this.Start(enableDebugMode, languagePack, null, null);
        }
        public bool Start(bool enableDebugMode, RateControlLanguagePack languagePack, Action<RateMyAppReminderStatus> callback, int[] exposurePatter)
        {
            var msg = new MessageBox();
            if (languagePack != null)
            {
                msg.SetLanguagePack(languagePack);
            }

            RateMyAppTask settings = new RateMyAppTask(new FileHelper(), msg, callback, exposurePatter);
            settings.Start(enableDebugMode);
            return true;
        }
    }
}
