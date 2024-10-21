using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameManager instance;
    [SerializeField] MonsterSpawner monsterSpawner;
    private void Awake()
    {
        instance = this;
    }
}
