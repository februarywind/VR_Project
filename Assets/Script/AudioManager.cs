using UnityEngine;

public enum SfxAudio
{
    HandGun, Door
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    [SerializeField] GameObject bgmObj;
    [SerializeField] AudioClip bgmClips;
    [Range(0, 1)] public float bgmVolum;
    [SerializeField] AudioSource bgmPlayer;

    [Header("SFX")]
    [SerializeField] GameObject sfxObj;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField][Range(0, 1)] float sfxVolum;
    public AudioSource[] sfxPlayer;

    private void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.volume = bgmVolum;
        bgmPlayer.clip = bgmClips;
        bgmPlayer.loop = true;

        sfxPlayer = new AudioSource[sfxClips.Length];
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayer[i].clip = sfxClips[i];
            sfxPlayer[i].volume = sfxVolum;
        }
    }
    public void PlaySfx(SfxAudio sfx)
    {
        //if (sfxPlayer[(int)sfx].isPlaying) return;
        sfxPlayer[(int)sfx].Play();
    }
    public void StopSfx(SfxAudio sfx)
    {
        if (!sfxPlayer[(int)sfx].isPlaying) return;
        sfxPlayer[(int)sfx].Stop();
    }
    public void StartBgm()
    {
        bgmPlayer.Play();
    }
    public void StopBgm()
    {
        bgmPlayer.Stop();
    }
    public void StopAllSfx()
    {
        foreach (var item in sfxPlayer)
        {
            item.Stop();
        }
    }
}
