using HypeFire.Library.Utilities.Extensions.Object;
using UnityEngine;

namespace HypeFire.Library.Utilities.Singleton
{
    /// <summary>
    /// Singleton niteliği eklenmek istenilen MonoBehaviour yapıları için temel sınıftır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance = null;
        private static readonly object padlock = new object();
        public bool isDontDestroyOnLoad;
        public static T GetInstance() => CreateOrFind();


        private static T CreateOrFind()
        {
            var isMissing = ReferenceEquals(_instance, null) ? false : (_instance ? false : true);
            if (isMissing || _instance.IsNull() || _instance.gameObject.IsNull())
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                {
                    _instance = objs[^1];
                    return _instance;
                }
                else
                {
                    GameObject go = new GameObject
                    {
                        name = typeof(T).ToString(),
                        hideFlags = HideFlags.DontSave
                    };
                    _instance = go.AddComponent<T>();
                }
            }

            return _instance;
        }

        public virtual void Awake()
        {
            if (_instance.IsNotNull() && _instance.gameObject.IsNotNull())
            {
                if (!_instance.gameObject.Equals(this.gameObject))
                    Destroy(this.gameObject);
                else if (isDontDestroyOnLoad)
                    DontDestroyOnLoad(this.gameObject);
            }
        }

        protected MonoBehaviourSingleton()
        {
        }
    }
}