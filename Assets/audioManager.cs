using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
