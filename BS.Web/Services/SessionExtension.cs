using Newtonsoft.Json;

public static class SessionExtensions
{
    private const string UserSessionKey = "ap_usk";

    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public static void SetCurrentUser(this ISession session, USER_SESSION user)
    {
        session.Set(UserSessionKey, user);
    }

    public static USER_SESSION GetCurrentUser(this ISession session)
    {
        return session.Get<USER_SESSION>(UserSessionKey);
    }
    public static T GetObject<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
    }
}
