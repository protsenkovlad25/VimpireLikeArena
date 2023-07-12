using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VampireLike.General
{
    public class PoolBehaviour<T> where T : MonoBehaviour
    {
        public T prefab;
        private Transform m_Parent;
        private List<T> m_Objects;

        public PoolBehaviour()
        {
            m_Objects = new List<T>();
        }

        public T Take()
        {
            foreach (var item in m_Objects)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    continue;
                }

                item.gameObject.SetActive(true);
                return item;
            }
            AddItemInPool();

            m_Objects.Last().gameObject.SetActive(true);
            return m_Objects.Last();
        }

        public void ReturnToPool(T obj)
        {
            if (m_Parent != null)
            {
                obj.transform.SetParent(m_Parent);
            }

            obj.gameObject.SetActive(false);
        }


        void AddItemInPool()
        {
            var clone = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);

            if (m_Parent != null)
            {
                clone.transform.SetParent(m_Parent);
            }

            clone.gameObject.SetActive(false);
            m_Objects.Add(clone);

        }

        public void Pooling(T obj, int count)
        {
            prefab = obj;
            for (int i = 0; i < count; i++)
            {
                AddItemInPool();
            }
        }

        public void ReturnAllPull()
        {
            foreach (var item in m_Objects)
            {
                item.gameObject.SetActive(false);

                if (m_Parent != null)
                {
                    item.transform.SetParent(m_Parent);
                }
            }
        }

        public void CreateParent(Transform parent)
        {
            m_Parent = parent;
        }
    }
}