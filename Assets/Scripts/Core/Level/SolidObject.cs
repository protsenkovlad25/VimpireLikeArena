using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidObject : MonoBehaviour
{
    private void Start()
    {
        EventManager.OnAllWavesSpawned.AddListener(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
