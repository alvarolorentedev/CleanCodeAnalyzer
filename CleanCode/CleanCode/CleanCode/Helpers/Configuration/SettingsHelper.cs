using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Helpers.Configuration
{
    public interface ISettingsHelper
    {

    }

    class SettingsHelper : ISettingsHelper
    {

        private const string SettingsFileName = "stylecop.json";
    }
}
