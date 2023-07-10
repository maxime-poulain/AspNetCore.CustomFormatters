using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers;
using System.Reflection;
using Facade;
using FormatterNewtonSoftJson.Extensions;

namespace FormatterNewtonSoftJson.Formatters;

public class FacadeOutputFormatter : NewtonsoftJsonOutputFormatter
{
    private static readonly Assembly FacadeAssembly = typeof(FacadeController).Assembly;

    public FacadeOutputFormatter(
        ArrayPool<char> charPool,
        MvcOptions mvcOptions,
        MvcNewtonsoftJsonOptions? jsonOptions) : base(GetSettings(jsonOptions!.SerializerSettings), charPool, mvcOptions,
        jsonOptions)
    {
    }

    public override bool CanWriteResult(OutputFormatterCanWriteContext context)
    {
        var actionDescriptor = context.HttpContext
            .GetEndpoint()
            ?.Metadata
            .GetMetadata<ControllerActionDescriptor>();

        if (actionDescriptor?.ControllerTypeInfo.Assembly == FacadeAssembly)
        {
            return base.CanWriteResult(context);
        }

        return false;
    }

    private static JsonSerializerSettings GetSettings(JsonSerializerSettings original)
    {
        var settings = original.Clone();
        settings.TypeNameHandling = TypeNameHandling.All;
        settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
        return settings;
    }
}
