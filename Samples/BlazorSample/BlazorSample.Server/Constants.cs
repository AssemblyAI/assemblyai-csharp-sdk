using Microsoft.AspNetCore.Components;

namespace BlazorSample.Server;

public class Constants
{
    public static readonly IComponentRenderMode RenderMode =
        Microsoft.AspNetCore.Components.Web.RenderMode.InteractiveWebAssembly;
}