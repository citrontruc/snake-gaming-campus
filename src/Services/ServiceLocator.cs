/* An object to register all the services used in the game. */

public static class Services
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        if (_services.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Service of type {typeof(T)} is already registered.");
        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.");
        return (T)_services[typeof(T)];
    }
}
