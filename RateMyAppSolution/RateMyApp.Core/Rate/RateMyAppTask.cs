using RateMyApp.Core.Abstractions.Storage;
using RateMyApp.Core.Abstractions.UI;
using RateMyApp.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.Core.Rate
{
    public class RateMyAppTask
    {
        RateMyAppSettings settings = null;
        Action<RateMyAppReminderStatus> _callback = null;
        IMessageBox messageBox = null;
        bool enableDebugMode = false;
        int[] exposurePattern = null;

        public RateMyAppTask(IFile fileInstance, IMessageBox messageBox)
            : this(fileInstance, messageBox, null, null)
        { }

        public RateMyAppTask(IFile fileInstance, IMessageBox messageBox, int[] exposurePattern)
            : this(fileInstance, messageBox, null, null)
        { }
        public RateMyAppTask(IFile fileInstance, IMessageBox messageBox, Action<RateMyAppReminderStatus> callback, int[] exposurePattern)
        {
            this.messageBox = messageBox;
            settings = new RateMyAppSettings(fileInstance);
            _callback = callback;
            this.exposurePattern = exposurePattern;
        }

        public void Start(bool enableDebugMode)
        {
            this.enableDebugMode = enableDebugMode;
            InitializeRateMyAppTask();
        }

        private void RaiseCallback(RateMyAppReminderStatus status)
        {
            if (_callback != null)
            {
                _callback(status);
            }
        }

        async private void InitializeRateMyAppTask()
        {
            var count = await settings.GetRateMyAppExposureCount();
            var runCount = await settings.GetAppRunsCount();
            runCount++;
            await settings.SetAppRunsCount(runCount);

            if (await settings.GetHasCompeletedEmailFeedback() || await settings.GetHasCompeletedRating())
            {
                // pop event to stats
                RaiseCallback(RateMyAppReminderStatus.Completed);
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(ExposurePattern.DELAY_ACTIVE_RUN));
                var activeappRuns = await settings.GetActiveAppRunsCount();
                bool show = true;
                if (exposurePattern != null && exposurePattern.Any())
                {
                    show = exposurePattern.Contains(activeappRuns);
                }
                else
                {
                    show = ExposurePattern.DefaultPattern(activeappRuns);
                }
#if DEBUG
            if (enableDebugMode)
            {
                show = true;
            }
#endif
                if (show)
                {
                    RaiseCallback(RateMyAppReminderStatus.Show);
                    var result = await messageBox.Show();

                    if(result == MessageBoxResult.Cancel){
                        RaiseCallback(RateMyAppReminderStatus.Canceled);
                    }
                    else if(result == MessageBoxResult.Yes){
                        RaiseCallback(RateMyAppReminderStatus.Completed);
                    }
                    else if(result == MessageBoxResult.No){
                        RaiseCallback(RateMyAppReminderStatus.Canceled);
                    }
                    else if(result == MessageBoxResult.ThirdOption){
                        RaiseCallback(RateMyAppReminderStatus.EmailReview);
                    }

                }
                var activeAppRunsCount = await settings.GetActiveAppRunsCount();
                activeAppRunsCount++;
                await settings.SetActiveAppRunsCount(activeAppRunsCount);
            }
        }
    }
}
