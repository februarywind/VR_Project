using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Magazine : MonoBehaviour
{
    [SerializeField] int maxbulletCount;
    public int MaxbulletCount => maxbulletCount;
    public int bulletCount;
    private void OnEnable()
    {
        bulletCount = maxbulletCount;
    }
}
