using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public MonsterSpawner monsterSpawner;

    public int waveLevel;
    public int killCount;

    private void Awake()
    {
        instance = this;
    }
}
