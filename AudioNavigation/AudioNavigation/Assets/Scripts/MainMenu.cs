using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public static bool isTraining = true;

    public void PlayGame()
    {
        isTraining = false;       
        SceneManager.LoadScene("Game");
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
