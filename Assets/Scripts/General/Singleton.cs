using UnityEngine;

namespace General
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                    
                var newObject = new GameObject
                {
                    name = typeof(T).Name
                };

                _instance = newObject.AddComponent<T>();
                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
}
