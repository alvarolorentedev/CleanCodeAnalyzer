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
        //ISettings Settings { get; }
    }

    internal static class SettingsHelper 
    {

        private const string SettingsFileName = "CleanCode.json";

        //public ISettings Settings { get { return settings ?? GetSettings(); } }

        //private ISettings settings;

        internal static ISettings GetSettings(this SyntaxTreeAnalysisContext context)
        {
            return context.Options.GetSettings();
        }
        internal static ISettings GetSettings(this AnalyzerOptions options)
        {
            return GetSettings(options != null ? options.AdditionalFiles : ImmutableArray.Create<AdditionalText>());
        }


        private static ISettings GetSettings(ImmutableArray<AdditionalText> additionalFiles)
        {
            try
            {
                foreach (var additionalFile in additionalFiles)
                {
                    if (Path.GetFileName(additionalFile.Path).ToLowerInvariant() == SettingsFileName)
                    {
                        var root = JsonConvert.DeserializeObject<SettingsFile>(additionalFile.GetText().ToString());
                        return root.Settings;
                    }
                }
                //var root = JsonConvert.DeserializeObject<SettingsFile>((Path.GetFileName(SettingsFileName));
                //return root.Settings;
            }
            catch (JsonException ex)
            {
                
            }
            return new Settings();
        }
    }
}
