namespace General.Pool.System
{
    public interface IPoolCollection
    {
        public IPoolObject Get(string key);
        public T Get<T>(string key) where T : IPoolObject;
        public void Return(IPoolObject poolObject);
        public IPool GetPool(string key);
    }
}