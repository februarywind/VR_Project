using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct WaveLevel
{
    public int door;
    public float spawnTime;
    public int maxMonster;
}
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] WaveLevel[] waveLevel;
    [SerializeField] List<Transform> openDoor;
    WaitForSeconds spawnTime;
    Coroutine waveCoroutine = null;
    public void WaveStart(int level)
    {
        if (waveCoroutine != null) return;
        for (int i = 0; i < waveLevel[level].door; i++)
        {
            Transform temp = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)];
            if (openDoor.Contains(temp))
            {
                i--;
                continue;
            }
            openDoor.Add(temp);
        }
        spawnTime = new WaitForSeconds(waveLevel[level].spawnTime);
        waveCoroutine = StartCoroutine(WaveCoroutine(level));
    }
    public void WaveEnd()
    {
        StopCoroutine(waveCoroutine);
    }
    IEnumerator WaveCoroutine(int level)
    {
        int count = 0;
        while (true)
        {
            PoolManager.instance.Create(PoolEnum.Monster, openDoor[UnityEngine.Random.Range(0, openDoor.Count)]);
            count++;
            if (count >= waveLevel[level].maxMonster) break;
            yield return spawnTime;
        }
    }
}
