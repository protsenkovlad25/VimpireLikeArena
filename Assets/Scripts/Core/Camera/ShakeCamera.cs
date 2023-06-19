using DG.Tweening;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;

    public void Init()
    {
        EventManager.MainCharacterTakeDamage.AddListener(Shake);
    }

    public void Shake()
    {
        m_Camera.DOShakePosition(0.5f, new Vector3(1, 1, 1), 10);
    }
}
