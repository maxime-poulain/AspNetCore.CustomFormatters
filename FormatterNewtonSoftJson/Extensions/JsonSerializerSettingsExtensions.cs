using System.Globalization;
using Newtonsoft.Json;

namespace FormatterNewtonSoftJson.Extensions;

public static class JsonSerializerSettingsExtensions
{
    public static JsonSerializerSettings Clone(this JsonSerializerSettings original)
    {
        ArgumentNullException.ThrowIfNull(original);
        return new JsonSerializerSettings
        {
            ReferenceLoopHandling = original.ReferenceLoopHandling,
            MissingMemberHandling = original.MissingMemberHandling,
            ObjectCreationHandling = original.ObjectCreationHandling,
            NullValueHandling = original.NullValueHandling,
            DefaultValueHandling = original.DefaultValueHandling,
            PreserveReferencesHandling = original.PreserveReferencesHandling,
            TypeNameHandling = original.TypeNameHandling,
            MetadataPropertyHandling = original.MetadataPropertyHandling,
            TypeNameAssemblyFormatHandling = original.TypeNameAssemblyFormatHandling,
            ConstructorHandling = original.ConstructorHandling,
            DateFormatString = original.DateFormatString,
            MaxDepth = original.MaxDepth,
            Formatting = original.Formatting,
            DateFormatHandling = original.DateFormatHandling,
            DateTimeZoneHandling = original.DateTimeZoneHandling,
            DateParseHandling = original.DateParseHandling,
            FloatFormatHandling = original.FloatFormatHandling,
            FloatParseHandling = original.FloatParseHandling,
            StringEscapeHandling = original.StringEscapeHandling,
            Culture = (CultureInfo)original.Culture.Clone(),
            CheckAdditionalContent = original.CheckAdditionalContent,
            Converters = new List<JsonConverter>(original.Converters),
            ContractResolver = original.ContractResolver,
            EqualityComparer = original.EqualityComparer,
            ReferenceResolverProvider = original.ReferenceResolverProvider,
            TraceWriter = original.TraceWriter,
            SerializationBinder = original.SerializationBinder,
            Error = original.Error,
            Context = original.Context
        };
    }
}