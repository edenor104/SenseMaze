using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class TrainingScript : MonoBehaviour
{
    /*public class Maze{
    public string maze_name;
    public Vector3 initial_position;

        public Maze(string name, Vector3 initial_position)
        {
            this.maze_name = name;
            this.initial_position = initial_position;
        }
    }*/

    public GameObject mazes;
    MazeManager mm;
    LoggerScript ls;
    public List<Transform> all_mazes;
    public GameObject Goal;
    public static int mazeIndex = -1;
    private string temp_str;
    private Transform temp_trans;
    private AudioSource audioSource; // the audio source component attached to this game object



    

    public static string[] mazes_name_list =  new string[]  {"Training", "Training", "Training"};
    public static string[] conditions = new string[] {"all", "visual_only", "audio_only"};
    public GameObject player; // the player game object
    // audio clips to play for raycasting sounds
    public AudioClip[] audioClips;
    private static Vector3 maze2_intial_location = new Vector3(0.935f, 0.131f, 1.196f);
    public  static Vector3[] initial_position = {
        maze2_intial_location,
        maze2_intial_location,
        maze2_intial_location,
        maze2_intial_location,
        maze2_intial_location,
    };
    // AudioSource NextLevelSound = GameObject.FindWithTag("Floor").GetComponent<AudioSource>();
   // AudioSource audioSource = GameManager.GetComponent<AudioSource>();


    /*public GameManagerScript()
    {
        Maze maze1 = new maze("Maze1", new Vector3(0.935f, 0.131f, -1.19f));
        Maze maze2 = new maze("Maze2", new Vector3(0.935f, 0.131f, 1.196f));
        Maze maze3 = new maze("Maze3", new Vector3(0.889f, 0.132f, -1.22f));
        Maze maze4 = new maze("Maze4", new Vector3(0.889f, 0.131f, 1.22f));

        Maze[] mazes =  new Maze[]  {maze1, maze2, maze3, maze4, maze3, maze4, maze2, maze1, maze2, maze4, maze3, maze1, maze4, maze3, maze2, maze1 };
        this.mazes_name_list = new string[16];
        this.initial_position = new Vector3[16];
        for(int i=0; i<16; i++)
        {
            Maze maze = mazes[i];
            this.mazes_name_list[i] = maze.maze_name;
            this.initial_position[i] = maze.initial_position;
        };
    }*/



    // Start is called before the first frame update
    void Start()
    {
        mazeIndex = -1;
        //gameObject.layer uses only integers, but we can turn a layer name into a layer integer using LayerMask.NameToLayer()
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        Goal.layer = LayerIgnoreRaycast;
        Debug.Log("Current layer: " + gameObject.layer);
        mm = mazes.GetComponent<MazeManager>();
        audioSource = player.GetComponent<AudioSource>();
        MainMenu.isTraining = true;
        //isTraining = true;
        //mm.TagWalls();

        //ls = GameObject.FindWithTag("Logger").GetComponent<LoggerScript>();


        // Create list of the mazes
        //foreach (Transform child in mazes.transform)
        //{
        //    all_mazes.Add(child);
        //}

        // Psaudo-randomize the diffrent conditions and mazes
        //ShuffleTransformArray(all_mazes, initial_position);
        //ShuffleTransformArrayConditions(all_mazes, conditions);
        NextMaze();
    }


    public void ShuffleTransformArray(List<Transform> arr, Vector3[] pos)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(0, arr.Count);
            temp_trans = arr[rnd];
            Vector3 temp_pos = pos[rnd];
            arr[rnd] = arr[i];
            pos[rnd] = pos[i];
            arr[i] = temp_trans;
            pos[i] = temp_pos;


        }
    }

        public void ShuffleTransformArrayConditions(List<Transform> arr, string[] cond)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(0, arr.Count);
            string cond_temp = cond[rnd];
            cond[rnd] = cond[i];
            cond[i] = cond_temp;


        }
    }


    public void NextMaze()
    {

        AudioSource NextLevelSound = GameObject.FindWithTag("Floor").GetComponent<AudioSource>();
        Transform targetTransform = player.GetComponent<Transform>();
        CharacterController controller_player = player.GetComponent<CharacterController>();

        string[] mazes_names = new string[] {"Training","Training", "Training", "Training",
        "Training"};
        mazeIndex = (mazeIndex + 1);
        if((mazeIndex) == 3)
        {
        SceneManager.LoadScene("MainMenu");
        return;
        }
        print(mazeIndex);
        print(mazes_names[mazeIndex]);
        PlayerMovement.collision_number = 0;
        PlayerMovement.StartingTimeMaze = Time.time;
        NextLevelSound.Play();
        // activate random condition
        print(conditions);
        controller_player.enabled = false;
        targetTransform.position = initial_position[mazeIndex];
        targetTransform.rotation =  Quaternion.Euler(0f, -90f, 0f);
        controller_player.enabled = true;
        //print(mazes_name_list);
        //mm.ActivateMaze(mazes_names[mazeIndex]);
        mm.ActivateCondition(conditions[mazeIndex], mazes_names[mazeIndex]);


    }
    
    
    
    

}

