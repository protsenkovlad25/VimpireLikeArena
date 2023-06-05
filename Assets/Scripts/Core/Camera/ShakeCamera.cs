using DG.Tweening;
using UnityEngine;
using VampireLike.Core.Characters;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private MainCharacter m_MainCharacter;

    public void Init()
    {
        //m_MainCharacter.TakingDamage += Shake;
        EventManager.MainCharacterTakeDamage.AddListener(Shake);
    }

    public void Shake()
    {
        Camera.DOShakePosition(0.5f, new Vector3(1, 0, 0), 10);
    }
}
