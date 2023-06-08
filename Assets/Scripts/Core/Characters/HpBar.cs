using UnityEngine;
using VampireLike.Core.Characters;

public class HpBar: MonoBehaviour
{
    [SerializeField] GameObject Fill;

    Transform transformCamera;

    private int initialHealth;
    private float initialFillScale;

    private void Start()
    {
        transformCamera = Camera.main.transform;
        initialFillScale = Fill.transform.localScale.x;
    }

    public void Update()
    {
        transform.LookAt(transformCamera);
    }

    public void Init(int health)
    {
        initialHealth = health;
    }

    public void UpdateHp(int currentHealth)
    {
        float newScale;
        
        newScale = (initialFillScale * currentHealth) / initialHealth;
        Fill.transform.localScale = new Vector3(newScale, Fill.transform.localScale.y, Fill.transform.localScale.z);
    }
}
