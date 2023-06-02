using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Levels
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private List<Platfom> m_Platfoms;
        [SerializeField] private Transform m_StartPoint;


        public void Move(Vector3 position)
        {
            StartCoroutine(MoveCoroutine(position));
        }

        private IEnumerator MoveCoroutine(Vector3 position)
        {
            foreach (var item in m_Platfoms)
            {
                item.Move(position);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}