using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;

namespace VampireLike.Core.Levels
{

    public class DeadGround : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<GameCharacterBehaviour>(out var character))
            {
                character.Die();
            }
        }
    }
}