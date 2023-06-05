using DG.Tweening;
using UnityEngine;
using VampireLike.Core.Characters;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] Camera Camera;
    //[SerializeField] private MainCharacter m_MainCharacter;

    public void Init()
    {
        EventManager.MainCharacterTakeDamage.AddListener(Shake);
        //m_MainCharacter.TakingDamage += Shake;
    }

    public void Shake()
    {
        if (Camera != null)
        {
            Debug.LogError("Shake");
            Camera.main.DOShakePosition(0.5f, new Vector3(1, 0, 0), 10);
        }
        else if (Camera == null) Debug.LogError("Camera NULL");
        else Debug.LogError("Pizdec");
    }
}
