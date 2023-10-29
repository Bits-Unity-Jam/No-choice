namespace DataStorage
{
    public interface IStorage
    {
        public void SaveAs(string data, string pathAndName);
        public string Load(string path);
    }
}