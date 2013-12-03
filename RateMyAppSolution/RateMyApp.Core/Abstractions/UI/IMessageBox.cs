using RateMyApp.Core.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.Core.Abstractions.UI
{
    public interface IMessageBox
    {
        Task<MessageBoxResult> Show();
        void SetLanguagePack(RateControlLanguagePack languagePack);
    }
}
