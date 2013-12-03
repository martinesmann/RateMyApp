using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.Core.Rate
{
    internal class ExposurePattern
    {
        private static readonly int APP_DELAY_ACTIVE_RUN = 7;

        private static Random ran = new Random();
        internal static bool RandomPattern(int count)
        {
            return ran.Next(int.MaxValue) % 2 == 0;
        }

        internal static bool DEBUGPattern(int count)
        {
            return true;
        }

        internal static bool DefaultPattern(int count)
        {
            var result = (count == 3 || count == 4 || count == 7 || count == 12 || count == 20);
            return result;
        }

        internal static int DELAY_ACTIVE_RUN { get { return APP_DELAY_ACTIVE_RUN; } }

    }
}
