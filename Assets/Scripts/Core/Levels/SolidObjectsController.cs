using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VampireLike.Core;

public class SolidObjectsController : MonoBehaviour, IIniting
{
    [SerializeField] private GameObject m_WallMarkPrefab;

    private List<SolidObject> m_Walls;
    private List<GameObject> m_WallMarks;

    public void Init()
    {
        m_WallMarks = new List<GameObject>();
    }

    public void SetWalls(List<SolidObject> walls)
    {
        m_Walls = walls;
    }

    public void InitWalls()
    {
        foreach (var wall in m_Walls)
        {
            wall.transform.position += new Vector3(0, 50, 0);
            wall.gameObject.SetActive(true);
        }
    }

    public void SetMark()
    {
        foreach (var wall in m_Walls)
        {
            RaycastHit[] hits = Physics.RaycastAll(wall.transform.position, new Vector3(0, -1, 0), 100f);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.TryGetComponent(out OnColiderEnterComponent c))
                {
                    GameObject mark;
                    SpriteRenderer markRenderer;

                    mark = Instantiate(m_WallMarkPrefab, hit.point + new Vector3(0, .1f, 0), m_WallMarkPrefab.transform.rotation);
                    mark.transform.rotation = Quaternion.Euler(mark.transform.eulerAngles.x, wall.transform.eulerAngles.y, mark.transform.eulerAngles.z);

                    markRenderer = mark.GetComponent<SpriteRenderer>();
                    markRenderer.size = new Vector2(wall.transform.localScale.x, wall.transform.localScale.z);

                    if (mark != null) m_WallMarks.Add(mark);

                    break;
                }
            }
        }
    }

    public void Landing()
    {
        foreach (var wall in m_Walls)
        {
            wall.transform.DOMoveY(1.6f, .8f).SetEase(Ease.InQuad);
        }
    }
}
