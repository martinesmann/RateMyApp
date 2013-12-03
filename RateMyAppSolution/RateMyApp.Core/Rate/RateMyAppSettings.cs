using RateMyApp.Core.Abstractions.Storage;
using RateMyApp.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.Core.Rate
{
    public class RateMyAppSettings : AppSettingsBase
    {
            private static readonly string APP_SETTINGS_KEY = "review-app-reminder-settings-";
            private static readonly string APP_SETTINGS_KEY_EXPOSURE_COUNT = APP_SETTINGS_KEY + "app-exposure-count";
            private static readonly string APP_SETTINGS_KEY_ACTIVE_APP_RUNS_COUNT = APP_SETTINGS_KEY + "app-active-app-runs-count";
            private static readonly string APP_SETTINGS_KEY_APP_RUNS_COUNT = APP_SETTINGS_KEY + "app-app-runs-count";
            private static readonly string APP_SETTINGS_KEY_REVIEW_COMPLETED = APP_SETTINGS_KEY + "app-review-completed";
            private static readonly string APP_SETTINGS_KEY_EMAIL_COMPLETED = APP_SETTINGS_KEY + "app-email-completed";
            //private Windows.Storage.ApplicationDataContainer localSettings = null;

            public RateMyAppSettings(IFile fileInstance) : base(fileInstance)
            {
            }

            async public Task<int> GetRateMyAppExposureCount()
            {

                int count = 0;
                var value = await base.GetValue(APP_SETTINGS_KEY_EXPOSURE_COUNT);
                if (value != null)
                {
                    int.TryParse(value.ToString(), out count);
                }
                return count;
            }

            async public Task SetRateMyAppExposureCount(int value)
            {
                await base.SetValue(APP_SETTINGS_KEY_EXPOSURE_COUNT, value);
            }

            async public Task<int> GetActiveAppRunsCount()
            {
                int count = 0;
                var value = await base.GetValue(APP_SETTINGS_KEY_ACTIVE_APP_RUNS_COUNT);
                if (value != null)
                {
                    int.TryParse(value.ToString(), out count);
                }
                return count;
            }
            async public Task SetActiveAppRunsCount(int value)
            {
                await base.SetValue(APP_SETTINGS_KEY_ACTIVE_APP_RUNS_COUNT, value);
            }

            async public Task<int> GetAppRunsCount()
            {
                int count = 0;
                var value = await base.GetValue(APP_SETTINGS_KEY_APP_RUNS_COUNT);
                if (value != null)
                {
                    int.TryParse(value.ToString(), out count);
                }
                return count;
            }
            async public Task SetAppRunsCount(int value)
            {
                await base.SetValue(APP_SETTINGS_KEY_APP_RUNS_COUNT, value);
            }

            async public Task<bool> GetHasCompeletedRating()
            {
                bool result = false;
                var value = await base.GetValue(APP_SETTINGS_KEY_REVIEW_COMPLETED);
                if (value != null)
                {
                    bool.TryParse(value.ToString(), out result);
                }
                return result;
            }
            async public Task SetHasCompeletedRating(bool value)
            {
                await base.SetValue(APP_SETTINGS_KEY_REVIEW_COMPLETED, value);
            }

            async public Task<bool> GetHasCompeletedEmailFeedback()
            {
                bool result = false;
                var value = await base.GetValue(APP_SETTINGS_KEY_EMAIL_COMPLETED);
                if (value != null)
                {
                    bool.TryParse(value.ToString(), out result);
                }
                return result;
            }
            async public Task SetHasCompeletedEmailFeedback(bool value)
            {
                await base.SetValue(APP_SETTINGS_KEY_EMAIL_COMPLETED, value);
            }

            async public Task<bool> ResetRateMyAppHistorieAsync()
            {
                try
                {
                    await this.SetRateMyAppExposureCount(0);
                    await this.SetHasCompeletedRating(false);
                    await this.SetHasCompeletedEmailFeedback(false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
}
