using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public int coreHp;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.GetComponent<Monster>().HitDmg(100);
        UIManger.Instance.CoreHpUpdate(--coreHp);
        if (coreHp <= 0) GameManager.instance.GameOver();
    }
}
