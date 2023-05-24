using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class LoggerScript : MonoBehaviour
{
    private Vector3[] initial_positions = GameManagerScript.initial_position;
    private string[] MazeType = GameManagerScript.mazes_name_list;
    private string[] ConditionType = GameManagerScript.conditions;
    private float samplingTime = 0.02f; // sample time in sec
    private string date_string = DateTime.Now.ToString("-dd-MM-yyyy_hh-mm-ss");    
    
    // Start is called before the first frame update
    void Start()
    {
        string filePath = Application.dataPath + "/" + "Data" + "/" + "Mazes" + "-" + "RawData" + date_string + ".csv";
        string filePath1 = Application.dataPath + "/" + "Data" + "/" + "Mazes-Summary" + date_string +  ".csv";
        File.AppendAllText(filePath1, "maze_type, audio_type, collision_number,success_maze,completion_time" + Environment.NewLine);
        File.AppendAllText(filePath, "time,x,y,z,rx,ry,rz,maze_type,audio_type" + Environment.NewLine);
    }

    public void OnEnable()
    {
        InvokeRepeating("SampleNow", 0, samplingTime);
    }

    public void ReachLocation(string[] MazeType, string[] ConditionType, int collision_number, float MazeSolveTime)
    {
        int success = 1;
        int maze_index = PlayerMovement.mazeIndex1; 
        string filePath1 = Application.dataPath + "/" + "Data" + "/" + "Mazes-Summary" + date_string +  ".csv";
        string logText = string.Format("{0},{1},{2},{3},{4}", MazeType[maze_index], ConditionType[maze_index], collision_number, success, MazeSolveTime);
        File.AppendAllText(filePath1, logText + Environment.NewLine);
    }

    public void FailedReachLocation(string[] MazeType, string[] ConditionType, int collision_number, float MazeSolveTime)
    {
        int success = 0;
        int maze_index = PlayerMovement.mazeIndex1; 
        string filePath1 = Application.dataPath + "/" + "Data" + "/" + "Mazes-Summary" + date_string +  ".csv";
        string logText = string.Format("{0},{1},{2},{3},{4}", MazeType[maze_index], ConditionType[maze_index], collision_number, success, MazeSolveTime);
        File.AppendAllText(filePath1, logText + Environment.NewLine);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void SampleNow()
    {
        int maze_index = PlayerMovement.mazeIndex1; 
        string timestamp =  Time.time.ToString();
        string filePath = Application.dataPath + "/" + "Data" + "/" + "Mazes" + "-" + "RawData" + date_string + ".csv";
        string logText = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", timestamp, transform.position.x, transform.position.y, transform.position.z, transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z, MazeType[maze_index], ConditionType[maze_index]);
        File.AppendAllText(filePath, logText + Environment.NewLine);
    }

}
