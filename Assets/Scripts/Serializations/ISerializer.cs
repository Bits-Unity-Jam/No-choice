using Chunks.ChunkRedactor;
using Newtonsoft.Json;

public interface ISerializer
{
    public string Serialize(object obj);

    public T Deserialize<T>(string str);
    
    string Serialize(object obj, object settings);
}