using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePitchRaycast : MonoBehaviour
{
    public float minDistance = 3.0f; // the distance at which the audio pitch will change
    public float triggerColision = 0.09f; // the distance at which the audio pitch will change
    public float minPitch = 1.0f; // the minimum audio pitch
    public float maxPitch = 2.0f; // the maximum audio pitch

    private AudioSource audioSource; // the audio source component attached to this game object
    private AudioSource audioSource2; // the audio source component attached to this game object
    private RaycastHit hit; // the raycast hit info
    private PauseMenu pauseMenu;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        audioSource2 = GameObject.FindWithTag("GameManager").GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Awake()
    {
    }

    void Update()
    {
        // Check if the game is paused
        if (pauseMenu.isPaused)
        {
            audioSource.volume = 0.0f;
            return; // Skip the rest of the code in the Update function
        }
        // shoot a raycast from the player in the forward direction
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            audioSource.volume = 0.5f;
            //print(hit.distance);
            // calculate the pitch based on the distance to the hit object
            if (hit.distance < minDistance)
            {
                if (hit.distance < triggerColision)
                {
                    if (!audioSource2.isPlaying)
                    {
                        audioSource2.Play();
                    }
                }
                float pitch = maxPitch * ((minDistance - Mathf.Abs(hit.distance)) / minDistance) + 1;
                // set the audio source pitch
                audioSource.pitch = pitch;
            }
            else {
                audioSource.volume = 0.0f;
            }
        }

    }
}