using Microsoft.JSInterop;

#pragma warning disable CS8614

namespace PoC.Blazor.SSR.Server.Mocks;

public class JSRuntime : IJSRuntime
{
    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object[] args) => new();

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object[] args) => new();
}