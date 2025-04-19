using Microsoft.JSInterop;
using System.Text.Json.Serialization.Metadata;

namespace IDK;

public class NoOpLocalStorageService : ILocalStorageService
{
    void ILocalStorageService.Clear() { }
    TValue? ILocalStorageService.GetItem<TValue>(string key, JsonTypeInfo<TValue>? jsonTypeInfo) where TValue : default => default;
    string? ILocalStorageService.Key(double index) => default;
    void ILocalStorageService.RemoveItem(string key) { }
    void ILocalStorageService.SetItem<TValue>(string key, TValue value, JsonTypeInfo<TValue>? jsonTypeInfo) { }
    double ILocalStorageService.Length => 0;
}
