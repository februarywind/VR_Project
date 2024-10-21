using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolEnum
{
    Bullet, Monster, Magazine
}
public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField] GameObject[] prefabs;
    [SerializeField] List<GameObject>[] pools;
    private void Awake()
    {
        instance = this;
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    public GameObject Create(PoolEnum pool, Transform point)
    {
        GameObject temp;
        foreach (var item in pools[(int)pool])
        {
            if (item.activeSelf) continue;
            item.transform.position = point.position;
            item.transform.rotation = point.rotation;
            item.SetActive(true);
            return item;
        }
        temp = Instantiate(prefabs[(int)pool], point.position, point.rotation);
        pools[(int)pool].Add(temp);
        return temp;
    }
}
