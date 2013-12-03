using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.Core.Rate
{
    public class RateControlLanguagePack
    {
        public RateControlLanguagePack()
        {
            this.RateMyAppCancelText = "RateMyAppCancelText";
            this.RateMyAppFeedbackEmail = "RateMyAppFeedbackEmail@hotmail.com";
            this.RateMyAppHeader = "RateMyAppHeader";
            this.RateMyAppText = "RateMyAppText";
            this.RateMyAppThridOptionText = "RateMyAppThridOptionText";
            this.RateMyAppYesText = "RateMyAppYesText";
        }
        public string RateMyAppCancelText { get; set; }
        public string RateMyAppFeedbackEmail { get; set; }
        public string RateMyAppHeader { get; set; }
        public string RateMyAppText { get; set; }
        public string RateMyAppThridOptionText { get; set; }
        public string RateMyAppYesText { get; set; }
    }
}
