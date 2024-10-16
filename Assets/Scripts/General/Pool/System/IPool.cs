namespace General.Pool.System
{
    public interface IPool
    {
        public IPoolObject Get();
        public T Get<T>() where T : IPoolObject;
        public void Return(IPoolObject poolObject);
    }
}