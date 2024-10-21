using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    [SerializeField] Transform objectSpawn;
    public void MagazineCreate()
    {
        PoolManager.instance.Create(PoolEnum.Magazine, objectSpawn);
    }
}
