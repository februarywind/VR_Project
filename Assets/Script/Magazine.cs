using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Magazine : MonoBehaviour
{
    [SerializeField] int maxbulletCount;
    [SerializeField] GameObject ViewBullet;
    public int MaxbulletCount => maxbulletCount;
    public int bulletCount;
    private void OnEnable()
    {
        bulletCount = maxbulletCount;
        ViewBullet.SetActive(true);
    }
    public void ZeroBullet()
    {
        ViewBullet.SetActive(false);
    }
}
