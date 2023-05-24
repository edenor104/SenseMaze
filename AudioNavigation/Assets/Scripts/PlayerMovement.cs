using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;



public class PlayerMovement : MonoBehaviour
{
    GameManagerScript gms;
    MazeManager mm;
    LoggerScript ls;
    public GameObject GameManager;
    public GameObject Mazes;
    public CharacterController controller;
    private AudioClip[] audioClips;
    [SerializeField] private GameManagerScript GameManagerScript;
    private Vector3[] initial_positions = GameManagerScript.initial_position;
    public float speed = 8f;
    public static int mazeIndex1 = 0;
    public static int collision_number = 0;
    private string[] MazeType = GameManagerScript.mazes_name_list;
    private string[] ConditionType = GameManagerScript.conditions;
    string filePath;

    //LoggerScript ls;
    float SavedTime = 0;
    float DelayTime = 1;
    float MazeSolveTime = 0;
    public static float StartingTimeMaze = 0;


    // For logging position

    private void Start()
    {
        //DateTime date = DateTime.Now;
        //string filePath = Application.dataPath + "/" + "Data" + "/" + "Mazes" + "-" + "RawData" + ".csv";
        //File.AppendAllText(filePath, "time,x,y,z,rx,ry,rz,maze_type,audio_type" + Environment.NewLine);

       // string filePath1 = Application.dataPath + "/" + "Data" + "/" + "Mazes-Summary" + ".csv";
        //File.AppendAllText(filePath1, "maze_type, audio_type, collision_number,success_maze,completion_time" + Environment.NewLine);

        controller.enabled = true;
    }



    private void Awake()
    {
        transform.position = initial_positions[0];
    }

    // Update is called once per frame
    void Update()
    {   
        mm = Mazes.GetComponent<MazeManager>();
        ls = GameObject.FindWithTag("Logger").GetComponent<LoggerScript>();
        gms = GameManager.GetComponent<GameManagerScript>();
        AudioSource NextLevelSound = GameObject.FindWithTag("Floor").GetComponent<AudioSource>();



        //float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Vector3 move = transform.right * x + transform.forward * z;
        Vector3 move = transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // log player details every x seconds
        if ((Time.time - SavedTime) > DelayTime)
        {
            SavedTime = Time.time;
            MazeSolveTime = Time.time - StartingTimeMaze;

            if (MazeSolveTime > 120)
            {
                Vector3[] initial_positions = GameManagerScript.initial_position;
                ls.ReachLocation(MazeType, ConditionType, collision_number, MazeSolveTime);
                //collision_number = 0;
                //StartingTimeMaze = Time.time;
                //AudioClip[] audioClips1 = mm.audioClips;
                //audioClips = audioClips1;
                //other.gameObject.SetActive(false);
                controller.enabled = false;
                gms.NextMaze();
                //mazeIndex1 = (mazeIndex1 + 1) % 16;
                //print(initial_position[mazeIndex1]);
                transform.position = initial_positions[mazeIndex1];
                //udioSource audioSource = GameManager.GetComponent<AudioSource>();
                //audioSource.clip = audioClips[mazeIndex1];
                controller.enabled = true;
                //NextLevelSound.Play();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ls = GameObject.FindWithTag("Logger").GetComponent<LoggerScript>();
        mm = Mazes.GetComponent<MazeManager>();
        gms = GameManager.GetComponent<GameManagerScript>();
        AudioSource audioSource = GameManager.GetComponent<AudioSource>();
        //AudioSource NextLevelSound = GameObject.FindWithTag("Floor").GetComponent<AudioSource>();
        AudioClip[] audioClips1 = mm.audioClips;



        if (other.gameObject.CompareTag("Finish"))
        {

            if (mazeIndex1 == 16) 
            {
                Application.Quit();
            }


            ls.ReachLocation(MazeType, ConditionType, collision_number, MazeSolveTime);
            //collision_number = 0;
            //StartingTimeMaze = Time.time;
            //controller.enabled = false;
            //mazeIndex1 = (mazeIndex1 + 1) % 16;
            //transform.position = initial_positions[mazeIndex1];
            gms.NextMaze();
            //controller.enabled = true;

        }

        if (other.gameObject.CompareTag("SoundWall"))
        {
            print(1);
            collision_number = collision_number + 1;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("InvisiWall"))
        {
            print(1);
            collision_number = collision_number + 1;
            audioSource.Play();
        }

        if (other.gameObject.CompareTag("GhostSoundWall"))
        {
            print(1);
            collision_number = collision_number + 1;
            audioSource.Play();
        }

    }
}

