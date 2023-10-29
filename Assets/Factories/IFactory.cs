namespace Factories
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
