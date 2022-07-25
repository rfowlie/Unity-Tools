using UnityEngine;

namespace ObjectPooling
{
    //poolable gameobjects
    //return the instance ID of the gameobject instead of trying to pass just the generic poolable type
    public abstract class Poolable<T> : MonoBehaviour where T : Poolable<T>
    {
        public event System.Action<T> COMPLETE;
        private int id = 0;
        protected abstract string SetName();
        
        private void Start()
        {
            gameObject.name = $"Poolable-{SetName()}";
        }
        protected virtual void OnEventComplete(T poolable) { COMPLETE?.Invoke(poolable); }
    }
}

