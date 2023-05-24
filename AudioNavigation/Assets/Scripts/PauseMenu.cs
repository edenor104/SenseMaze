using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class PauseMenu : MonoBehaviour
{
    
    public GameObject PauseMenu1;
    public bool isPaused;
    public GameManagerScript gms;
    int fail = 0;
    private string[] MazeType = GameManagerScript.mazes_name_list;
    private string[] ConditionType = GameManagerScript.conditions;


    // Start is called before the first frame update
    void Start()
    {
        PauseMenu1.SetActive(false);
        gms = FindObjectOfType<GameManagerScript>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();   
            }
            else
            {
                PauseGame();
            }

        }
    }

    public void PauseGame()
    {
        PauseMenu1.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;  
    }

        public void NextMazeButton()
    {
        //string filePath1 = Application.dataPath + "/" + "Data" + "/" + "Mazes" + "-Summary" + ".csv";
        //string logText1 = string.Format("{0},{1},{2},{3},{4}", MazeType[mazeIndex], ConditionType[mazeIndex], collision_number, fail, MazeSolveTime);
       // File.AppendAllText(filePath1, logText1 + Environment.NewLine);
        gms.NextMaze();
        isPaused = false;
        Time.timeScale = 1f;  
    }

        public void ResumeGame()
    {
        PauseMenu1.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;  
  
    }

    public void GoToMainMenu()
    {
         Time.timeScale = 1f;
         SceneManager.LoadScene("MainMenu");   
    }


        public void QuitGame()
    {
         Application.Quit();
    }
}
