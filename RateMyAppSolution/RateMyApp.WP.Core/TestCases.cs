using RateMyApp.Core.Rate;
using RateMyApp.Core.Settings;
using RateMyApp.Core.SharedSource.Storage;
using RateMyApp.WP.Core.Platform.UI;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RateMyApp.WP.Core
{
    public class TestCases
    {
        async public Task<bool> Test1()
        {
            AppSettingsBase settings = new AppSettingsBase(new FileHelper());
            string value = "hej med dig";
            await settings.SaveJson(value);
            var result = await settings.GetJson();
            return result == value;
        }

        public bool Test2()
        {
            RateMyAppTask settings = new RateMyAppTask(new FileHelper(), new MessageBox());
            settings.Start(true);
            return true;
        }
    }
}
