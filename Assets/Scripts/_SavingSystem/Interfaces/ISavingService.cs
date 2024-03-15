public interface ISavingService
{
    T Load<T>(string key); 
    void Save<T>(string key, T value);
}