using AIDotNet.Abstractions;
using AIDotNet.LLamaSharp;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOllamaService(this IServiceCollection services)
    {
        IApiChatCompletionService.ServiceNames.Add("LLamaSharp", LLamaSharpOptions.ServiceName);
        services.AddKeyedSingleton<IApiChatCompletionService, LLamaSharpChatService>(LLamaSharpOptions.ServiceName);
        return services;
    }
}