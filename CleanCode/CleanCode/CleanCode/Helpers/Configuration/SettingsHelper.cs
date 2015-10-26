using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Helpers.Configuration
{
    interface ISettingsHelper
    {
        ISettings Settings { get; }
    }

    class SettingsHelper : ISettingsHelper 
    {

        private const string SettingsFileName = "CleanCode.json";

        public ISettings Settings { get { return settings ?? GetSettings(); } }

        private ISettings settings;

        private static ISettings GetSettings()
        {
            try
            {
                var root = JsonConvert.DeserializeObject<SettingsFile>(SettingsFileName);
                return root.Settings;
            }
            catch (JsonException)
            {
                return new Settings();
            }
        }
    }
}
