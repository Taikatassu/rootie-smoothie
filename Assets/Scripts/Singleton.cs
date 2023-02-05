using UnityEngine;

namespace  RootieSmoothie
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                var obj = new GameObject(typeof(T).Name);
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }
    
        public void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = GetComponent<T>();
            gameObject.name = typeof(T).Name;
            DontDestroyOnLoad(gameObject);
        }
    }
}
