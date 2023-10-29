using System.IO;
using UnityEngine;

namespace DataStorage
{
    public class SystemIOStorage : MonoBehaviour, IStorage
    {
        public void SaveAs(string data, string pathAndName) =>
            File.WriteAllText(Application.dataPath + pathAndName + ".json", data);

        public string Load(string path) => File.ReadAllText(Application.dataPath + path + ".json");
    }
}