using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour
{
    [SerializeField] AudioClip pwnd;
    [Min(1)][Tooltip("Around 1 announcemente in every X enemies killed")][SerializeField] int announRarety = 6;
    AudioSource aS;

    void Start()
    {
        aS = GetComponent<AudioSource>();
        aS.clip = pwnd;
    }

    public void AnnouncePwnage()
    {
        int procPwnd = Random.Range(0, announRarety);
        if (procPwnd == (announRarety-1))
        {
            aS.Play();
        }
    }
}
