using System.Buffers;
using System.Reflection;
using Facade;
using FormatterNewtonSoftJson.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;

namespace FormatterNewtonSoftJson.Formatters;

public class FacadeInputFormatter : NewtonsoftJsonInputFormatter
{
    private static readonly Assembly FacadeAssembly = typeof(FacadeController).Assembly;

    public FacadeInputFormatter(
        ILogger logger,
        ArrayPool<char> charPool,
        ObjectPoolProvider objectPoolProvider,
        MvcOptions options,
        MvcNewtonsoftJsonOptions jsonOptions) : base(logger,
        GetSettings(jsonOptions.SerializerSettings),
        charPool,
        objectPoolProvider,
        options,
        jsonOptions)
    {
    }

    public override bool CanRead(InputFormatterContext context)
    {
        var actionDescriptor = context.HttpContext
            .GetEndpoint()
            ?.Metadata
            .GetMetadata<ControllerActionDescriptor>();

        if (actionDescriptor?.ControllerTypeInfo.Assembly == FacadeAssembly)
        {
            return base.CanRead(context);
        }

        return false;
    }

    private static JsonSerializerSettings GetSettings(
        JsonSerializerSettings jsonOptionsSerializerSettings)
    {
        var settings = jsonOptionsSerializerSettings.Clone();
        settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
        settings.TypeNameHandling = TypeNameHandling.All;
        return settings;
    }
}