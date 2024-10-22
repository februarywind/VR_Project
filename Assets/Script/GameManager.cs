using System.Collections;
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

    public void doorDown(Transform[] doors)
    {
        StartCoroutine(DoorMove());
        IEnumerator DoorMove()
        {
            while (true)
            {
                foreach (var item in doors)
                {
                    item.Translate(Vector3.down * Time.deltaTime);
                }
                if (doors[0].position.y < 1) break;
                yield return null;
            }
        }
    }
    public void doorUp(Transform[] doors)
    {
        StartCoroutine(DoorMove());
        IEnumerator DoorMove()
        {
            while (true)
            {
                foreach (var item in doors)
                {
                    item.Translate(Vector3.up * Time.deltaTime);
                }
                if (doors[0].position.y > 4.46) break;
                yield return null;
            }
        }
    }
}
