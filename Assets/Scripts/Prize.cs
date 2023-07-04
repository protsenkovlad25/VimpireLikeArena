using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VampireLike.Core.Characters;
using VampireLike.Core.Weapons;

public class Prize : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out MainCharacter mc))
        {
            EventManager.WeaponReceived(WeaponType.TestWeapon, mc);
            Destroy(gameObject);
        }
    }
}
