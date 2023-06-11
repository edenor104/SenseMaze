using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;



public class PlayerMovement : MonoBehaviour
{
    GameManagerScript gms;
    TrainingScript ts;
    PauseMenu pm;
    MazeManager mm;
    LoggerScript ls;
    public GameObject GameManager;
    public GameObject StartSound;
    public GameObject Mazes;
    public CharacterController controller;
    //private AudioClip[] audioClips;
    [SerializeField] private GameManagerScript GameManagerScript;
    private Vector3[] initial_positions = GameManagerScript.initial_position;
    public static int collision_number = 0;
    public static int collision_status = 0;

    public float speed = 8f;
    int mazeIndex = GameManagerScript.mazeIndex;
    private string[] MazeType = GameManagerScript.mazes_name_list;
    private string[] ConditionType = GameManagerScript.conditions;
    string filePath;

    //LoggerScript ls;
    float SavedTime = 0;
    float DelayTime = 1;
    public static float MazeSolveTime = 0;
    public static float StartingTimeMaze = 0;


    // For logging position

    private void Start()
    {
        controller.enabled = true;
        
    }



    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        mm = Mazes.GetComponent<MazeManager>();
        if(MainMenu.isTraining)
            {

            }
        else
            {
                ls = GameObject.FindWithTag("Logger").GetComponent<LoggerScript>();
            }
        gms = GameManager.GetComponent<GameManagerScript>();
        ts = GameManager.GetComponent<TrainingScript>();
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
                if(MainMenu.isTraining)
                {
                    ts.NextMaze();

                }
                else
                {
                    ls.FailedReachLocation(MazeType, ConditionType, collision_number, MazeSolveTime);
                    gms.NextMaze();
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pm = GameManager.GetComponent<PauseMenu>();
        AudioSource SuccessSound = GameObject.FindWithTag("Logger").GetComponent<AudioSource>();
        if(!MainMenu.isTraining)
        {
            ls = GameObject.FindWithTag("Logger").GetComponent<LoggerScript>();

        }
            AudioSource audioSource = GameManager.GetComponent<AudioSource>();
            AudioSource audioSource2 = StartSound.GetComponent<AudioSource>();

        //AudioClip[] audioClips1 = mm.audioClips;

            if (other.gameObject.CompareTag("Finish")){
            GameObject finishObject = GameObject.FindGameObjectWithTag("Finish");
            AudioSource finishSound = finishObject.GetComponentInChildren<AudioSource>();
            /*float startVolume = finishSound.volume;
		    while (finishSound.volume > 0) {
			finishSound.volume -= startVolume * Time.deltaTime;
		    }*/
		    finishSound.Stop();
            

                if(MainMenu.isTraining)
                {
                    //ts.NextMaze();
                    SuccessSound.Play();
                    //finishSound.volume = startVolume;
                    pm.NextLevelScreen();
                }
                else
                {
                    ls.ReachLocation(MazeType, ConditionType, collision_number, MazeSolveTime);
                    SuccessSound.Play();
                    //finishSound.volume = startVolume;
                    pm.NextLevelScreen();
                    //gms.NextMaze();
                }

            }

            if (other.gameObject.CompareTag("SoundWall"))
            {
                print(1);
                collision_number = collision_number + 1;
                collision_status = 1;
                //yield return new WaitForSeconds(1f); 
                audioSource.Play();
            }

            if (other.gameObject.CompareTag("InvisiWall"))
            {
                print(2);
                collision_number = collision_number + 1;
                collision_status = 2;
                //yield return new WaitForSeconds(1f); 
                audioSource.Play();
            }

            if (other.gameObject.CompareTag("MuteWall"))
            {
                print(3);
                collision_number = collision_number + 1;
                collision_status = 3;
                
            }

            if (other.gameObject.CompareTag("GhostWall"))
            {
                print(4);
                collision_status = 4;
            }

            if (other.gameObject.CompareTag("GhostSoundWall"))
            {
                print(5);
                collision_status = 5;
                audioSource.Play();
            }
            
            if (other.gameObject.CompareTag("StartingPoint"))
            {
                print(5);
                collision_status = 5;
                audioSource2.Play();
            }
            else
            {
                //collision_status = 0;

            }

        }

} 