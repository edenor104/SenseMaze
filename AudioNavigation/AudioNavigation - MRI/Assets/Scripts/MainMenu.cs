using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LSL;


public class MainMenu : MonoBehaviour
{
    public static bool isTraining = true;

    // For LSL EEG management
    string StreamName = "LSL4Unity.Samples.SimpleCollisionEvent";
    string StreamType = "Markers";
    public static StreamOutlet outlet;
    private string[] sample_start_all = {""};

    private void Start()
    {
        var hash = new Hash128();
        hash.Append(StreamName);
        hash.Append(StreamType);
        hash.Append(gameObject.GetInstanceID());
        StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, 1, LSL.LSL.IRREGULAR_RATE,
        channel_format_t.cf_string, hash.ToString());
        outlet = new StreamOutlet(streamInfo);
        
    }

    private void Update()
    {
        // Check for the T key press to start the game
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        isTraining = false;       
        SceneManager.LoadScene("Game");
        sample_start_all[0] = "start experiment";
        outlet.push_sample(sample_start_all);
    }

        public void QuitGame()
    {
        Application.Quit();     
    }

    public void ActivateTrainingMode()
    {
        isTraining = true;       
        SceneManager.LoadScene("TrainingLevel");
    }
}
