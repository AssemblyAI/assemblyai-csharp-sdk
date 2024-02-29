using System;
using Microsoft.Extensions.DependencyInjection;

namespace Sample;

public static class DiContainer
{
    public static ServiceProvider Services { get; private set; }
    public static void BuildServices(Action<ServiceCollection> serviceBuilder)
    {
        var collection = new ServiceCollection();
        serviceBuilder(collection);
        Services = collection.BuildServiceProvider();
    }
}