using System.Collections.Generic;
using UnityEngine;


namespace ObjectPooling
{
    public class ObjectPool<T> where T : Poolable<T>
    {
        private Dictionary<T, bool> active;
        private Queue<T> inactive;

        public ObjectPool(Transform parent, int amount)
        {
            active = new Dictionary<T, bool>();
            inactive = new Queue<T>(amount);
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.parent = parent;
                //all poolable objects should know how to construct themselves
                T poolable = obj.AddComponent<T>();
                poolable.COMPLETE += ReturnToPool;
                poolable.gameObject.SetActive(false);
                inactive.Enqueue(poolable);
            }
        }

        public void DestroyPool()
        {
            foreach(var o in inactive)
            {
                GameObject.Destroy(o);
            }
            foreach(var o in active.Keys)
            {
                GameObject.Destroy(o);
            }
        }

        private void ReturnToPool(T poolable)
        {
            //just in case...
            if (!active.ContainsKey(poolable))
            {
                Debug.LogError("Poolable object not found in active list!!");
                return;
            }

            active.Remove(poolable);
            inactive.Enqueue(poolable);
            poolable.gameObject.SetActive(false);
        }

        public T GetObject()
        {
            if (inactive.Count == 0) { return null; }
            T o = inactive.Dequeue();
            active.Add(o, true);
            return o;
        }
    }
}