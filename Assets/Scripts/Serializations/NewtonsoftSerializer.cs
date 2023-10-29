using Chunks.ChunkRedactor;
using Newtonsoft.Json;
using UnityEngine;

public class NewtonsoftSerializer : MonoBehaviour
{
    public class NewtonsoftJsonSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public string Serialize(object obj, object settings)
        {
            JsonSerializerSettings jsonSerializerSettings = settings as JsonSerializerSettings;
            
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSerializerSettings);
        }
    }
}