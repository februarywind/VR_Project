using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MonsterSpawner monsterSpawner;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject gameOverPanel;

    public int waveLevel;
    public int killCount;

    private void Awake()
    {
        instance = this;
    }
    public void GameStart(bool restart)
    {
        if (restart)
        {
            waveLevel = 0;
            killCount = 0;
            restart = false;
        }
        monsterSpawner.WaveStart();
    }
    public void GameOver()
    {
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"Game Over\nClear Wave : {waveLevel + 1}";
    }

    public void doorDown(Transform[] doors)
    {
        StartCoroutine(DoorMove());
        AudioManager.instance.PlaySfx(SfxAudio.Door);
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
        AudioManager.instance.PlaySfx(SfxAudio.Door);
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
