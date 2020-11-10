namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ChannelsData
    {
        [JsonProperty("Channels")]
        public string[] Channels { get; set; }
    }

    public partial class ChannelsData
    {
        public static ChannelsData FromJson(string json) => JsonConvert.DeserializeObject<ChannelsData>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ChannelsData self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}