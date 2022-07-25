using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extentions
{
    public static class Array
    {
        //better than system.Array.Resize because will instantiate the array if not instantiated...
        public static T[] Resize<T>(T[] arr, int newSize)
        {
            T[] temp = new T[newSize];
            for (int i = 0; i < temp.Length; i++)
            {
                //resizing to a greater length, set values to default for type
                if (i > arr.Length - 1)
                {
                    temp[i] = default(T);
                }
                //otherwise just swap over value so no data lost
                else
                {
                    temp[i] = arr[i];
                }
            }

            return temp;
        }

        //remove at index
        public static T[] Remove<T>(T[] arr, int index)
        {
            if(index < 0 || index > arr.Length - 1)
            {
                Debug.LogError("Index was outside the range of the passed in array");
                return arr;
            }

            List<T> t = new List<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                if(i != index)
                {
                    t.Add(arr[i]);
                }
            }

            return t.ToArray();
        }
    }
}


