using System;
using Newtonsoft.Json;

namespace CleanCode.Helpers.Configuration
{
    public interface ISettingsBase
    {
    }

    public interface ISettings : ISettingsBase
    {
        [JsonProperty("methodSettings", DefaultValueHandling = DefaultValueHandling.Include)]
        IMethodSettings MethodSettings { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class Settings : ISettings
    {
        public IMethodSettings MethodSettings { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public interface IMethodSettings : ISettingsBase
    {
        [JsonProperty("cyclomaticComplexity", DefaultValueHandling = DefaultValueHandling.Include)]
        byte CyclomaticComplexity { get; set; }

        [JsonProperty("numberOfParameters", DefaultValueHandling = DefaultValueHandling.Include)]
        byte NumberOfParameters { get; set; }

        [JsonProperty("linesOfCode", DefaultValueHandling = DefaultValueHandling.Include)]
        short LinesOfCode { get; set; }
    }

    class MethodSettings : IMethodSettings
    {
        public byte CyclomaticComplexity { get; set; }

        public byte NumberOfParameters { get; set; }

        public short LinesOfCode { get; set; }
    }
}
