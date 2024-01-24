using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using LSL;



public class PlayerMovement : MonoBehaviour
{
    GameManagerScript gms;
    TrainingScript ts;
    MazeManager mm;
    PauseMenu pm;
    LoggerScript ls;
    public GameObject GameManager;
    public GameObject StartSound;
    public GameObject Mazes;
    public CharacterController controller;
    //private AudioClip[] audioClips;
    [SerializeField] private GameManagerScript GameManagerScript;
    //private Vector3[] initial_positions = GameManagerScript.initial_position;
    public static int collision_number = 0;
    public static int collision_status = 0;

    public float speed = 8f;
    int mazeIndex = GameManagerScript.mazeIndex;
    private string[] MazeType;
    private string[] ConditionType;
    string filePath;

    //LoggerScript ls;
    float SavedTime = 0;
    float DelayTime = 1;
    public static float MazeSolveTime = 0;
    public static float StartingTimeMaze = 0;


    // For LSL EEG management
    private string[] sample = {""};
    private StreamOutlet outlet_player;

    private void Start()
    {
        controller.enabled = true;
        outlet_player = MainMenu.outlet;
        
    }



    private void Awake()
    {
        outlet_player = MainMenu.outlet;
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
                //Vector3[] initial_positions = GameManagerScript.initial_position;
                if(MainMenu.isTraining)
                {
                    pm.NextLevelScreen();

                }
                else
                {
                    collision_status = 8;
                    sample[0] = "Time Run Out";
                    outlet_player.push_sample(sample);
                    //mm.TriggerRotationStop();
                    MazeType = GameManagerScript.mazes_name_list;
                    ConditionType = GameManagerScript.conditions;
                    ls.FailedReachLocation(MazeType, ConditionType, collision_number, MazeSolveTime);
                    pm.NextLevelScreen();
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        outlet_player = MainMenu.outlet;
        pm = GameManager.GetComponent<PauseMenu>();
        mm = Mazes.GetComponent<MazeManager>();
        AudioSource SuccessSound = GameObject.FindWithTag("Logger").GetComponent<AudioSource>();
        if(!MainMenu.isTraining)
        {
            ls = GameObject.FindWithTag("Logger").GetComponent<LoggerScript>();

        }
            AudioSource audioSource = GameManager.GetComponent<AudioSource>();
            AudioSource audioSource2 = StartSound.GetComponent<AudioSource>();

        //AudioClip[] audioClips1 = mm.audioClips;

        if (other.gameObject.CompareTag("Finish")){
            print(mm);
            //mm.TriggerRotationStop();
            collision_status = 7;
            GameObject finishObject = GameObject.FindGameObjectWithTag("Finish");
            AudioSource finishSound = finishObject.GetComponentInChildren<AudioSource>();

            sample[0] = "Finish ";
            Debug.Log(sample[0]);
            outlet_player.push_sample(sample);
            /*float startVolume = finishSound.volume;
		    while (finishSound.volume > 0) {
			finishSound.volume -= startVolume * Time.deltaTime;
		    }*/
		    finishSound.Stop();
            /*foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
                {

                    MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
                    if (meshRenderer != null){
                        meshRenderer.enabled = true;
                    }
                }*/
            

                if(MainMenu.isTraining)
                {
                    //ts.NextMaze();
                    SuccessSound.Play();
                    //finishSound.volume = startVolume;
                    pm.NextLevelScreen();
                }
                else
                {
                    MazeType = GameManagerScript.mazes_name_list;
                    int index_maze = GameManagerScript.mazeIndex; 
                    ConditionType = GameManagerScript.conditions;
                    ls.ReachLocation(MazeType, ConditionType, collision_number, MazeSolveTime);
                    SuccessSound.Play();
                    //finishSound.volume = startVolume;
                    pm.NextLevelScreen();
                    //gms.NextMaze();
                }

            }

            if (other.gameObject.CompareTag("SoundWall"))
            {

                if((MainMenu.isTraining))
                {                    
                    print(1);
                    collision_number = collision_number + 1;
                    collision_status = 1;
                }

                else
                {
                    int index_maze = GameManagerScript.mazeIndex;
                    ConditionType = GameManagerScript.conditions;
                    if(ConditionType[index_maze] == "const_contra_visual")
                        {
                            collision_number = collision_number + 1;
                            print(2);
                            sample[0] = "Collision InvisiWall";
                            //Debug.Log(sample[0]);
                            outlet_player.push_sample(sample); 
                            collision_status = 2;
                        }
                    
                    if(ConditionType[index_maze] == "const_contra_audio2")
                        {
                            collision_number = collision_number + 1;
                            sample[0] = "Collision MuteWall";
                            //Debug.Log(sample[0]);
                            outlet_player.push_sample(sample); 
                            print(3);
                            collision_status = 3;
                        }

                    if(ConditionType[index_maze] == "invisible")
                        {
                            print(2);
                            collision_number = collision_number + 1;
                            collision_status = 2;
                            sample[0] = "Collision InvisiWall";
                            //Debug.Log(sample[0]);
                            outlet_player.push_sample(sample); 
                            audioSource.Play();
                        }
            
                    else
                        {
                            print(1);
                            collision_number = collision_number + 1;
                            collision_status = 1;
                            sample[0] = "Collision SoundWall";
                            //Debug.Log(sample[0]);
                            outlet_player.push_sample(sample); 
                        }
                }
                audioSource.Play();
            }

            if (other.gameObject.CompareTag("InvisiWall"))
            {
                print(2);
                collision_number = collision_number + 1;
                collision_status = 2;
                sample[0] = "Collision InvisiWall";
                //Debug.Log(sample[0]);
                outlet_player.push_sample(sample); 
                audioSource.Play();
            }

            if (other.gameObject.CompareTag("MuteWall"))
            {
                print(3);
                collision_number = collision_number + 1;
                sample[0] = "Collision MuteWall";
                //Debug.Log(sample[0]);
                outlet_player.push_sample(sample); 
                collision_status = 3;
                
            }

            if (other.gameObject.CompareTag("GhostWall") || other.gameObject.CompareTag("PhantomWall"))
            {
                print(4);
                sample[0] = "Collision GhostWall";
                //Debug.Log(sample[0]);
                outlet_player.push_sample(sample); 
                collision_status = 4;
            }

            if (other.gameObject.CompareTag("GhostSoundWall") || other.gameObject.CompareTag("PhantomAudioWall"))
            {
                print(5);
                sample[0] = "Collision GhostSoundWall";
                //Debug.Log(sample[0]);
                outlet_player.push_sample(sample); 
                collision_status = 5;
                audioSource.Play();
            }
            
            if (other.gameObject.CompareTag("StartingPoint"))
            {
                print(6);
                sample[0] = "StartingPointCollision";
                //Debug.Log(sample[0]);
                outlet_player.push_sample(sample); 
                collision_status = 6;
                audioSource2.Play();
            }

            if (other.gameObject.CompareTag("AudioGoal"))
            {
                print(9);
                sample[0] = "AudioGoalRadius";
                //Debug.Log(sample[0]);
                outlet_player.push_sample(sample); 
                collision_status = 9;
            }
            else
            {
                //collision_status = 0;

            }

        }

} 