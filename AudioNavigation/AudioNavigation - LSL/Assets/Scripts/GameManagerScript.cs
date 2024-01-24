using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;



public class GameManagerScript : MonoBehaviour
{

    public GameObject mazes;
    MazeManager mm;
    LoggerScript ls;
    TrainingScript ts;
    PauseMenu pm;
    public GameObject NextLevelCanvas1;
    public List<Transform> all_mazes;
    public GameObject Goal;
    public static int mazeIndex = -1;
    public static int count = 0;
 
    private string temp_str;
    private Transform temp_trans;
    private AudioSource audioSource; // the audio source component attached to this game object
    private string[] MazeType_temp = new string[]  {} ;
    private string[] ConditionType_temp = new string[]  {} ;
    //[SerializeField] private string GameHardMode;

    public TextMeshProUGUI textMeshPro;
    public static string[] mazes_name_list =  new string[]  {} ;
    public static string[] conditions = new string[] {};
    public GameObject player; // the player game object
    // audio clips to play for raycasting sounds
    public AudioClip[] audioClips;
    private static Vector3 maze1_intial_location = new Vector3(0.62f, 0.219f, -1.23f);
    private static Vector3 maze2_intial_location = new Vector3(0.62f, 0.219f, 0.83f);
    private static Vector3 maze3_intial_location = new Vector3(0.62f, 0.219f, 0.83f);
    private static Vector3 maze4_intial_location = new Vector3(-0.59f, 0.219f, 0.86f); 

    void Awake()
    {
         // Get the currently loaded scene
        //Scene currentScene = SceneManager.GetActiveScene();
        string csvFilePath = Application.dataPath + "/Maze_conds.csv";

        // Compare the name of the current scene with the target scene name
        //if (currentScene.name == GameHardMode)
        //{
        //    csvFilePath = Application.dataPath + "/Maze_conds_hard.csv";
        //}
        int[] maze_vector = ReadCSV<int>(csvFilePath);
        Console.WriteLine(string.Join(" ", maze_vector));
        (MazeType_temp, ConditionType_temp) = GenerateMazeArrays(maze_vector);
        print(MazeType_temp.Length);
        mazes_name_list = MazeType_temp;
        print(mazes_name_list.Length);
        conditions = ConditionType_temp;
        UpdateTextNextLevelScreen(conditions[0]);

    }
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
                if (obj.CompareTag("MuteWall")  || obj.CompareTag("PhantomWall")|| obj.CompareTag("Finish") || obj.CompareTag("Player") || obj.CompareTag("StartingPoint") ||  obj.CompareTag("GhostWall") || obj.CompareTag("AudioGoal"))
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
            if ((mazeIndex) ==  0)
            {
                //NextLevelCanvas1.SetActive(true);
                //Time.timeScale = 0f;

            }
            if ((mazeIndex +1) ==  mazes_name_list.Length)
            {

            }    
               
            else
            {
                UpdateTextNextLevelScreen(conditions[mazeIndex + 1]);
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
        switch (mazes_name_list[mazeIndex])
                {
                    case "Maze1" when true:
                    targetTransform.position = maze1_intial_location;
                        break;
                    case "Maze2" when true:
                    targetTransform.position = maze2_intial_location;
                        break;
                    case "Maze3" when true:
                    targetTransform.position = maze3_intial_location;
                        break;
                    case "Maze4" when true:
                    targetTransform.position = maze4_intial_location;
                        break;
                }
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

    public (string[], string[]) GenerateMazeArrays(int[] mazeNumbers)
    {
        string[] mazeNames = new string[mazeNumbers.Length];
        string[] mazeConditions = new string[mazeNumbers.Length];
        
        for (int i = 0; i < mazeNumbers.Length; i++)
        {
            int mazeNumber = mazeNumbers[i];
            
            int mazeIndex = mazeNumber / 10;
            int conditionIndex = mazeNumber % 10;
            string mazeName = "Maze" + mazeIndex;
            switch (conditionIndex)
                {
                    case 1:
                    mazeConditions[i] = "audio_only";
                        break;
                    case 2:
                    mazeConditions[i] = "visual_only";
                        break;
                    case 3:
                    mazeConditions[i] = "contra_visual";
                        break;
                    case 4:
                    mazeConditions[i] = "contra_audio";
                        break;
                    case 5:
                    mazeConditions[i] = "all";
                        break;
                    case 6:
                    mazeConditions[i] = "invisible";
                        break;
                    case 7:
                    mazeConditions[i] = "const_contra_audio2";
                        break;
                    case 8:
                    mazeConditions[i] = "const_contra_visual";
                        break;
                    case 9:
                    mazeConditions[i] = "all";
                        break;
                    case 0:
                    mazeConditions[i] = "const_contra_visual_rotation";
                        break;

                }            
            mazeNames[i] = mazeName;
        }
        
        return (mazeNames, mazeConditions);
    }
    
    public T[] ReadCSV<T>(string relativeFilePath)
    {
        string csvFilePath = Path.Combine(Environment.CurrentDirectory, relativeFilePath);
        string[] lines = File.ReadAllLines(csvFilePath);

        int rowCount = lines.Length;
        int columnCount = lines[0].Split(',').Length;
        T[,] matrix = new T[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            string[] fields = lines[i].Split(',');

            for (int j = 0; j < columnCount; j++)
            {
                if (TryParseValue<T>(fields[j], out T value))
                {
                    matrix[i, j] = value;
                }
                else
                {
                    // Handle parsing errors if needed
                }
            }
        }

        System.Random random = new System.Random();
        int randomRowIndex = random.Next(0, rowCount);
        T[] randomRow = new T[columnCount];

        for (int j = 0; j < columnCount; j++)
        {
            randomRow[j] = matrix[randomRowIndex, j];
        }

        return randomRow;
    }

    private bool TryParseValue<T>(string input, out T value)
    {
        try
        {
            value = (T)Convert.ChangeType(input, typeof(T));
            return true;
        }
        catch
        {
            value = default(T);
            return false;
        }
    }

  public void UpdateTextNextLevelScreen(string condition_name)
    {
      switch (condition_name)
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
                    case "const_contra_visual_rotation" when true:
                    textMeshPro.text = "Trust Audio, Visual Could Be Misleading";
                        break;
                    case "const_contra_audio_rotation" when true:
                    textMeshPro.text = "Trust Visual, Audio Could Be Misleading";
                        break;
                    case "const_contra_visual_no_audio" when true:
                    textMeshPro.text = "No Audio, Visual Could Be Misleading";
                        break;
                }  
    }
    

}




