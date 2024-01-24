using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePitchRaycast : MonoBehaviour
{
    public float minDistance = 3.0f; // the distance at which the audio pitch will change
    public float triggerColision = 0.002f; // the distance at which the audio pitch will change
    public float minPitch = 1.0f; // the minimum audio pitch
    public float maxPitch = 2.0f; // the maximum audio pitch
    //private float delayTimer = 0.0f;

    //int j = 0;

    private AudioSource audioSource; // the audio source component attached to this game object
    private AudioSource audioSource2; // the audio source component  for game mode
    private AudioSource audioSource3; // the audio source component for training mode

    public static RaycastHit hit; // the raycast hit info
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
        
        if(MainMenu.isTraining)
        {
            int index = (TrainingScript.mazeIndex % 3);
        if (TrainingScript.conditions[index] == "visual_only")
            {
            audioSource.volume = 0.0f;
            if ((hit.distance < triggerColision))
                {
                    if (!audioSource2.isPlaying)
                    {
                        audioSource2.Play();
                    }
                }
            Physics.Raycast(transform.position, transform.forward, out hit);
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
                if ((hit.distance < triggerColision))
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
        else{
        if (GameManagerScript.mazeIndex != -1)
        {    
            if (GameManagerScript.conditions[GameManagerScript.mazeIndex] == "visual_only" || GameManagerScript.conditions[GameManagerScript.mazeIndex] == "const_contra_visual_no_audio")
                {
                audioSource.volume = 0.0f;
                if ((hit.distance < triggerColision))
                    {
                        if (!audioSource2.isPlaying)
                        {
                            audioSource2.Play();
                        }
                    }
                Physics.Raycast(transform.position, transform.forward, out hit);
                return; // Skip the rest of the code in the Update function

                }
            }


        /*if (GameManagerScript.conditions[GameManagerScript.mazeIndex] == "const_contra_audio")
        {      
        if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
            
            float[] randomNumber_list = {2f, 0.5f, 0f, 3f, 1.5f, 0.5f, 1.2f, 0.7f};
            float[] time_waiting = {2f, 1f, 4f, 2f, 5f, 2f, 2.5f, 3f};
            delayTimer += Time.deltaTime;
            if (delayTimer >= time_waiting[j])
            {
            // Execute your desired action after the specified delay duration
            float pitch = maxPitch * ((Mathf.Abs(hit.distance)) / ((Mathf.Abs(hit.distance))+1.5f)) + randomNumber_list[j];
            audioSource.pitch =  pitch;              // set the audio source pitch
            j = (j + 1) % randomNumber_list.Length;
            // Reset timer for the next delay
             delayTimer = 0.0f;
            }
            return; // Skip the rest of the code in the Update function
        
            }
        }*/
                    // shoot a raycast from the player in the forward direction
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            audioSource.volume = 0.5f;
            //print(hit.distance);
            // calculate the pitch based on the distance to the hit object
            if (hit.distance < minDistance)
            {
                if ((hit.distance < triggerColision))
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
}