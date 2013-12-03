using RateMyApp.Core.Rate;
using RateMyApp.Core.Settings;
using RateMyApp.Core.SharedSource.Storage;
using RateMyApp.WS.Core.Platform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.WS.Core
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


        public bool Test2(string packageFamilyName)
        {
            RateMyAppTask task = new RateMyAppTask(new FileHelper(), new MessageBox(packageFamilyName));
            task.Start(true);
            

            return true;
        }
    }
}
