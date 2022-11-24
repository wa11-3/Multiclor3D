using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioEfects;

    private void Start()
    {
        Debug.Log("TEST");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("IN");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OUT");
    }
}
