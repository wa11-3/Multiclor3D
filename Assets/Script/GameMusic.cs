using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioSource audioPlayer;


    public static GameMusic Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {

    }

    public void StopMusic()
    {

    }

    public void ChangeMusic()
    {

    }
}
