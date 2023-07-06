using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core.Characters;

public abstract class PickapblePrize : MonoBehaviour
{
    public Action OnGet;

    public abstract void Initialize();

    public abstract void GetPrize(MainCharacter mainCharacter);

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out MainCharacter mc))
        {
            GetPrize(mc);
            OnGet();
        }
    }
}
