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
        m_Walls = new List<SolidObject>();
        EventManager.OnAllWavesSpawned.AddListener(DestroyWalls);
    }

    public void SetWalls(List<SolidObject> walls)
    {
        foreach (var wall in walls)
        {
            m_Walls.Add(wall);
        }
    }

    public void InitWalls(List<SolidObject> walls)
    {
        foreach (var wall in walls)
        {
            for (int i = 0; i < m_Walls.Count; i++)
            {
                if (wall == m_Walls[i])
                {
                    wall.transform.position += new Vector3(0, 50, 0);
                    wall.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetMark(List<SolidObject> walls)
    {
        m_WallMarks = new List<GameObject>();

        foreach (var wall in walls)
        {
            for (int i = 0; i < m_Walls.Count; i++)
            {
                if (wall == m_Walls[i])
                {
                    RaycastHit[] hits = Physics.RaycastAll(m_Walls[i].transform.position, new Vector3(0, -1, 0), 100f);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.collider.TryGetComponent(out OnColiderEnterComponent c))
                        {
                            GameObject mark;
                            SpriteRenderer markRenderer;

                            mark = Instantiate(m_WallMarkPrefab, hit.point + new Vector3(0, .1f, 0), m_WallMarkPrefab.transform.rotation);
                            mark.transform.rotation = Quaternion.Euler(mark.transform.eulerAngles.x, m_Walls[i].transform.eulerAngles.y, mark.transform.eulerAngles.z);

                            markRenderer = mark.GetComponent<SpriteRenderer>();
                            markRenderer.size = new Vector2(m_Walls[i].transform.localScale.x, m_Walls[i].transform.localScale.z);

                            if (mark != null) m_WallMarks.Add(mark);

                            break;
                        }
                    }
                }
            }
        }
    }

    public void Landing(List<SolidObject> walls)
    {
        foreach (var wall in walls)
        {
            for (int i = 0; i < m_Walls.Count; i++)
            {
                if (wall == m_Walls[i])
                {
                    m_Walls[i].transform.DOMoveY(1.6f, .8f).SetEase(Ease.InQuad);
                }
            }
        }
    }

    public void DestroyWalls()
    {
        m_Walls.Clear();
    }
}
