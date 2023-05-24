using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePitchRaycast : MonoBehaviour
{
    public float minDistance = 3.0f; // the distance at which the audio pitch will change
    public float minPitch = 1.0f; // the minimum audio pitch
    public float maxPitch = 2.0f; // the maximum audio pitch

    private AudioSource audioSource; // the audio source component attached to this game object
    private RaycastHit hit; // the raycast hit info

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        // shoot a raycast from the player in the forward direction
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            audioSource.volume = 0.5f;
            //print(hit.distance);
            // calculate the pitch based on the distance to the hit object
            if (hit.distance < minDistance)
            {
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