using DG.Tweening;
using UnityEngine;
using VampireLike.Core.Characters;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] Camera Camera;

    public void Init()
    {
        EventManager.MainCharacterTakeDamage.AddListener(Shake);
    }

    public void Shake()
    {
        Camera.DOShakePosition(0.5f, new Vector3(1, 1, 1), 10);
    }
}
