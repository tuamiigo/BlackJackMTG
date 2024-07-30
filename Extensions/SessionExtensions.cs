using System.Text.Json;

public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }

    public static Task SetInt32Async(this ISession session, string key, int value)
    {
        return Task.Run(() => session.SetInt32(key, value));
    }
}
