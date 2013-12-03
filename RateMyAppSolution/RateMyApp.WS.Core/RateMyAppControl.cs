using RateMyApp.Core.Rate;
using RateMyApp.Core.SharedSource.Storage;
using RateMyApp.WS.Core.Platform.UI;
using System;

namespace RateMyApp.WS.Core
{
    public class RateMyAppControl
    {
        public bool Start(bool enableDebugMode, string packageFamilyName, RateControlLanguagePack languagePack)
        {
            return this.Start(enableDebugMode, packageFamilyName, languagePack, null, null);
        }

        public bool Start(bool enableDebugMode, string packageFamilyName, RateControlLanguagePack languagePack, Action<RateMyAppReminderStatus> callback, int[] exposurePatter)
        {
            var msg = new MessageBox(packageFamilyName);
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