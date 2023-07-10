using System.Buffers;
using FormatterNewtonSoftJson.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;

namespace FormatterNewtonSoftJson;

public class NewtonsoftMvcOptionsRegistration : IConfigureOptions<MvcOptions>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly MvcNewtonsoftJsonOptions _jsonOptions;
    private readonly ArrayPool<char> _charPool;
    private readonly ObjectPoolProvider _objectPoolProvider;

    public NewtonsoftMvcOptionsRegistration(
        ILoggerFactory loggerFactory,
        IOptions<MvcNewtonsoftJsonOptions> jsonOptions,
        ArrayPool<char> charPool,
        ObjectPoolProvider objectPoolProvider)
    {
        _loggerFactory = loggerFactory;
        _jsonOptions = jsonOptions.Value;
        _charPool = charPool;
        _objectPoolProvider = objectPoolProvider;
    }

    public void Configure(MvcOptions options)
    {
        options.OutputFormatters.Insert(0,
            new FacadeOutputFormatter(_charPool, options, _jsonOptions));

        var inputLogger = _loggerFactory.CreateLogger<FacadeInputFormatter>();
        options.InputFormatters.Insert(0, new FacadeInputFormatter(
            inputLogger,
            _charPool,
            _objectPoolProvider,
            options,
            _jsonOptions));
    }
}

public static class FacadeFormatterMvcBuilderExtensions
{
    public static IMvcBuilder AddFacadeFormatters(this IMvcBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor
            .Transient<IConfigureOptions<MvcOptions>, NewtonsoftMvcOptionsRegistration>());
        return builder;
    }
}