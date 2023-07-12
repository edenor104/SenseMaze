using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;



public class GameManagerScript : MonoBehaviour
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
    TrainingScript ts;

    public List<Transform> all_mazes;
    public GameObject Goal;
    public static int mazeIndex = -1;
    public static int count = 0;
    private string temp_str;
    private Transform temp_trans;
    private AudioSource audioSource; // the audio source component attached to this game object
    public TextMeshProUGUI textMeshPro;




    

    public static string[] mazes_name_list =  new string[]  {"Maze1", "Maze2", "Maze3", "Maze1", "Maze4", "Maze1", "Maze2", "Maze1",
    "Maze3", "Maze4", "Maze2", "Maze1", "Maze2", "Maze4", "Maze3", "Maze4",
    "Maze2", "Maze4", "Maze3", "Maze1", "Maze3", "Maze2", "Maze3", "Maze2",
     "Maze4", "Maze3", "Maze2", "Maze1", "Maze4",  "Maze1", "Maze4","Maze3",
     "Maze2", "Maze1", "Maze3", "Maze4" } ;
    /*public static string[] conditions = new string[] {"const_contra_audio", "const_contra_visual", "const_contra_visual", "const_contra_visual", "const_contra_audio2",
     "const_contra_audio2", "const_contra_audio2", "const_contra_audio2", "audio_only", "all",
      "contra_visual", "visual_only", "invisible", "contra_visual", "all",
       "contra_audio", "contra_visual", "audio_only","visual_only", "all",
       "invisible","contra_audio", "audio_only", "invisible"};*/
    public static string[] conditions = new string[] {"invisible", "visual_only", "contra_audio", "const_contra_visual", "audio_only", "all", "const_contra_audio", "const_contra_audio2",
     "visual_only", "contra_visual", "contra_audio", "audio_only", "const_contra_visual", "const_contra_audio", "all", "const_contra_audio2",
      "contra_visual", "visual_only", "invisible", "contra_visual", "const_contra_visual", "all", "const_contra_audio", "const_contra_audio2",
       "contra_audio", "contra_visual", "audio_only","visual_only","const_contra_visual",  "const_contra_audio", "all", "const_contra_audio2",
       "invisible","contra_audio", "audio_only", "invisible"};
    public GameObject player; // the player game object
    // audio clips to play for raycasting sounds
    public AudioClip[] audioClips;
    private static Vector3 maze1_intial_location = new Vector3(0.62f, 0.219f, -1.23f);
    private static Vector3 maze2_intial_location = new Vector3(0.62f, 0.219f, 0.83f);
    private static Vector3 maze3_intial_location = new Vector3(0.62f, 0.219f, 0.83f);
    private static Vector3 maze4_intial_location = new Vector3(-0.59f, 0.219f, 0.86f); 
    public  static Vector3[] initial_position = {
        maze1_intial_location, maze2_intial_location, maze3_intial_location, maze1_intial_location, maze4_intial_location, maze1_intial_location, maze2_intial_location, maze1_intial_location,
        maze3_intial_location, maze4_intial_location, maze2_intial_location, maze1_intial_location,  maze2_intial_location, maze4_intial_location, maze3_intial_location, maze4_intial_location,
        maze2_intial_location, maze4_intial_location, maze3_intial_location, maze1_intial_location,  maze3_intial_location, maze2_intial_location, maze3_intial_location, maze2_intial_location,
        maze4_intial_location, maze3_intial_location, maze2_intial_location, maze1_intial_location, maze4_intial_location, maze1_intial_location, maze4_intial_location, maze3_intial_location,
        maze2_intial_location, maze1_intial_location, maze3_intial_location, maze4_intial_location,
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
        //gameObject.layer uses only integers, but we can turn a layer name into a layer integer using LayerMask.NameToLayer()
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        Goal.layer = LayerIgnoreRaycast;
        Debug.Log("Current layer: " + gameObject.layer);
        mm = mazes.GetComponent<MazeManager>();
        audioSource = player.GetComponent<AudioSource>();
        MainMenu.isTraining = false;
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
        GameObject[] phantom_mazes = GameObject.FindGameObjectsWithTag("PhantomMaze");
        GameObject[] phantom_mazes2 = GameObject.FindGameObjectsWithTag("PhantomMazeAudio");
        int layer = LayerMask.NameToLayer("Default");


        
        foreach (GameObject phantom_maze in phantom_mazes) // make sure to turn off phantom visual mazes
        {
        if (phantom_maze != null)
            {
            phantom_maze.SetActive(false);
            }
        }

        foreach (GameObject phantom_maze2 in phantom_mazes2) // make sure to turn off phantom audio mazes
        {
        if (phantom_maze2 != null)
            {
            phantom_maze2.SetActive(false);
            }
        } 
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (obj.CompareTag("InvisiWall") || obj.CompareTag("PhantomAudioWall") || obj.CompareTag("GhostSoundWall") || obj.CompareTag("StartingPoint") ) // for setting obkect to return some objects as invisible
                {
                continue;
                }

                MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
                if (meshRenderer != null){
                    meshRenderer.enabled = true;
                }
            }

        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>()) // for setting obkect to return raycasting sound
            {
                if (obj.CompareTag("MuteWall")  || obj.CompareTag("PhantomWall")|| obj.CompareTag("Finish") || obj.CompareTag("Player") || obj.CompareTag("StartingPoint") ||  obj.CompareTag("GhostWall") )
                {
                continue;
                }

            // Set the layer of the child object
            obj.layer = layer;
            }

        // change next screen title according to a condition
        mazeIndex = (mazeIndex + 1) % mazes_name_list.Length;
        print(mazeIndex);
        count = count + 1;
        if (count ==  mazes_name_list.Length + 1) 
            {
            Application.Quit();
            return;
            }
                print("Count: " + count);
        print(conditions[mazeIndex]);
        if (textMeshPro != null)
        {
            if ((mazeIndex +1) ==  mazes_name_list.Length)
            {

            }    
               
            else
            {
                switch (conditions[mazeIndex + 1])
                {
                    case "audio_only" when true:
                    textMeshPro.text = "Next Maze: Only Audio";
                        break;
                    case "visual_only" when true:
                    textMeshPro.text = "Next Maze: Only Visual";
                        break;
                    case "contra_visual" when true:
                    textMeshPro.text = "Trust Audio, Visual Could Be Misleading";
                        break;
                    case "contra_audio" when true:
                    textMeshPro.text = "Trust Visual, Audio Could Be Misleading";
                        break;
                    case "all" when true:
                    textMeshPro.text = "Trust Visual and Audio";
                        break;
                    case "invisible" when true:
                    textMeshPro.text = "Trust Audio, Visual Could Be Misleading";
                        break;
                    case "const_contra_audio" when true:
                    textMeshPro.text = "Trust Visual, Audio Could Be Misleading";
                        break;
                    case "const_contra_audio2" when true:
                    textMeshPro.text = "Trust Visual, Audio Could Be Misleading";
                        break;
                    case "const_contra_visual" when true:
                    textMeshPro.text = "Trust Audio, Visual Could Be Misleading";
                        break;

                }
            }
        }
        print("Index Maze after Addition:" + mazeIndex);
        print(mazes_name_list[mazeIndex]);
        PlayerMovement.collision_number = 0;
        PlayerMovement.StartingTimeMaze = Time.time;
        NextLevelSound.Play();
        // activate random condition
        //print(conditions);
        controller_player.enabled = false;
        targetTransform.position = initial_position[mazeIndex];
        if(mazes_name_list[mazeIndex] == "Maze4")
        {
            targetTransform.rotation =  Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            targetTransform.rotation =  Quaternion.Euler(0f, -90f, 0f);
        }
        controller_player.enabled = true;
        //print(mazes_name_list);
        mm.ActivateMaze(mazes_name_list[mazeIndex]);
        mm.ActivateCondition(conditions[mazeIndex], mazes_name_list[mazeIndex]);


    }
    
    
    
    

}




