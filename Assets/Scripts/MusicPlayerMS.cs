using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerMS : MonoBehaviour
{
    [SerializeField] AudioClip inGameMusic;
    [SerializeField] float volume = 0.2f;
    AudioSource aS;
    private void Awake()
    {
        int nMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        int mMusicPlayerMSs = FindObjectsOfType<MusicPlayerMS>().Length;
        if (nMusicPlayers >= 1 || mMusicPlayerMSs > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            aS = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        Invoke("PlayInGameMusic", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        aS.volume = volume;
    }

    void PlayInGameMusic()
    {
        aS.volume = .5f;
        aS.clip = inGameMusic;
        aS.Play();
    }
}
