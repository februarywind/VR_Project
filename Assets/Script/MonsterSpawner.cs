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
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
        GameManager.instance.doorDown(spawnPoint);
    }
    public void WaveStart()
    {
        if (waveCoroutine != null) return;
        for (int i = 0; i < waveLevel[gameManager.waveLevel].door; i++)
        {
            Transform temp = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)];
            if (openDoor.Contains(temp))
            {
                i--;
                continue;
            }
            openDoor.Add(temp);
        }
        spawnTime = new WaitForSeconds(waveLevel[gameManager.waveLevel].spawnTime);
        waveCoroutine = StartCoroutine(WaveCoroutine(gameManager.waveLevel));
        gameManager.killCount = 0;
    }
    public void WaveClear()
    {
        gameManager.waveLevel++;
        gameManager.waveLevel = Math.Min(gameManager.waveLevel, 9);
        StopCoroutine(waveCoroutine);
        waveCoroutine = null;
        GameManager.instance.doorDown(openDoor.ToArray());
        openDoor.Clear();
    }
    public void KillMonster()
    {
        gameManager.killCount++;
        if (gameManager.killCount < waveLevel[gameManager.waveLevel].maxMonster) return;
        WaveClear();
    }
    IEnumerator WaveCoroutine(int level)
    {
        int count = 0;
        GameManager.instance.doorUp(openDoor.ToArray());
        while (count < waveLevel[level].maxMonster)
        {
            PoolManager.instance.Create(PoolEnum.Monster, openDoor[UnityEngine.Random.Range(0, openDoor.Count)]);
            count++;
            yield return spawnTime;
        }
    }
}
