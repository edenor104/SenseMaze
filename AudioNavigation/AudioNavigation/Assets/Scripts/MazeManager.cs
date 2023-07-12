using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public static AudioClip[] audioClips;
    public Camera mainCamera; // Drag the main camera object into this field in the inspector
    //public AudioListener listenerObject;
    public GameObject ObscureObj;
    public GameObject player;
    private string activeTag = "OuterWall"; 



    public void ActivateMaze(string curr_maze_name)
    {
        print("Maze - " + curr_maze_name);
        // Enable only current maze walls
        foreach (Transform maze in transform)
        {
            if (maze.name == curr_maze_name)
            {
                maze.gameObject.SetActive(true);
            }
            else
            {
                maze.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateCondition(string condition_name, string maze_name)
    {
        print(maze_name);
            
        switch (condition_name)
        {
            case "audio_only" when true:
                ApplyAudioOnly(maze_name);
                break;
            case "visual_only" when true:
                ApplyVisualOnly(maze_name);
                break;
            case "contra_visual" when true:
                ApplyContraVisual(maze_name);
                break;
            case "contra_audio" when true:
                ApplyContraAudio(maze_name);
                break;
            case "all" when true:
                ApplyVisualAudio(maze_name);
                break;
            case "invisible" when true:
                ApplyInvisible(maze_name);
                break;
            case "const_contra_audio" when true:
                ApplyConstContraAudio(maze_name);
                break;
            case "const_contra_visual" when true:
                ApplyConstContraVisual(maze_name);
                break;
            case "const_contra_audio2" when true:
                ApplyConstContraAudio2(maze_name);
                break;
        }
    }
    

    void ApplyAudioOnly(string maze_name)
    {
        AudioSource audioSource3 = player.GetComponent<AudioSource>();
        // Condition 1 - Audio Only
        // Make contra visual walls are off

    
        Transform maze_transform = GameObject.Find(maze_name).transform;
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object
        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }


            // make sure contra audio walls are off
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }


        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }
        
        

        print("audio");
        // Make sure Obstruction of view is on -NO VISION
        ObscureObj.SetActive(true);
        // Make sure listener is ON
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;

    }
    

    void ApplyVisualOnly(string maze_name)
    {
        // Condition 2 - Visual Only
        // Make contra visual walls are off
        AudioSource audioSource3 = player.GetComponent<AudioSource>();
        Transform maze_transform = GameObject.Find(maze_name).transform;
        // Make contra visual walls are off
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }
        // make sure contra audio walls are OFF
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }

        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }


        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is off - NO SOUND
        audioSource3.volume = 0f;
        //AudioListener.volume = 0f;
        print("visual");

    }

    void ApplyContraVisual(string maze_name)
    {
        AudioSource audioSource3 = player.GetComponent<AudioSource>();
        // Apply condition 3 - contra visual - visual is false, needs to count on Audio
        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is on
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
        // Make sure contra visual walls are ON
        Transform maze_transform = GameObject.Find(maze_name).transform;
        // Make contra visual walls are ON
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(true);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(true);
            }
        }
        // make sure contra audio walls are OFF
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }

        GameObject[] morph_wallS = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_wallS)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(false);
            }
        }

        print("contra_visual");

    }

    void ApplyContraAudio(string maze_name)
    {
        AudioSource audioSource3 = player.GetComponent<AudioSource>();
        Transform maze_transform = GameObject.Find(maze_name).transform;    
        // Apply condition 4 to the maze objects
        ObscureObj.SetActive(false);
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
        print("contra_audio");            
        // Make contra visual walls are off
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }
        // make sure contra audio walls are ON
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(true);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(true);
            }
        }

        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(false);
            }
        }

    }
        
    void ApplyVisualAudio(string maze_name)
    {
        AudioSource audioSource3 = player.GetComponent<AudioSource>();   
        // Condition 2 - Visual Only
        Transform maze_transform = GameObject.Find(maze_name).transform;
        // Make contra visual walls are off
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }

            // make sure contra audio walls are off
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }


        
        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }

        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is ON
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
        print("all");

    }


    void ApplyInvisible(string maze_name)
    {AudioSource audioSource3 = player.GetComponent<AudioSource>(); 
        // Condition 2 - Visual Only
        // Make contra visual walls are off
        Transform maze_transform = GameObject.Find(maze_name).transform;
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }

            // make sure contra audio walls are off
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }


        
        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }
        


        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is ON
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag(activeTag))
            {
                continue;
            }

            if (obj.CompareTag("Floor"))
            {
                continue;
            }

            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null){
                meshRenderer.enabled = false;
            }
        }
    }

    void ApplyConstContraAudio(string maze_name)
    {
        AudioSource audioSource3 = player.GetComponent<AudioSource>();   
        // Condition 2 - Visual Only
        Transform maze_transform = GameObject.Find(maze_name).transform;
        // Make contra visual walls are off
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }

            // make sure contra audio walls are off
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }


        
        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }

        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is ON
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
        print("const_audio_clash");

    }

    void ApplyConstContraVisual(string maze_name)
    {AudioSource audioSource3 = player.GetComponent<AudioSource>(); 
        // Condition 2 - Visual Only
        // Make contra visual walls are off
        Transform maze_transform = GameObject.Find(maze_name).transform;
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }

            // make sure contra audio walls are off
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }


        
        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }

        GameObject[] phantom_mazes = GetChildGameObjectsWithTag(maze_transform, "PhantomMaze"); // Replace "ChildObject" with the name of the child object
        foreach (GameObject phantom_maze in phantom_mazes)
        {
        if (phantom_maze != null)
            {
            phantom_maze.gameObject.SetActive(true);
            }
        }
        


        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is ON
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag(activeTag))
            {
                continue;
            }

            if (obj.CompareTag("Floor"))
            {
                continue;
            }

            if (obj.CompareTag("PhantomMaze"))
            {
                continue;
            }

            if (obj.CompareTag("PhantomGoal"))
            {
                continue;
            }

            if (obj.CompareTag("PhantomWall"))
            {
                continue;
            }
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null){
                meshRenderer.enabled = false;
            }
        }
        print("const_visual_clash");

    }        


    void ApplyConstContraAudio2(string maze_name)
    {AudioSource audioSource3 = player.GetComponent<AudioSource>(); 
        // Condition 2 - Visual Only
        // Make contra visual walls are off
        Transform maze_transform = GameObject.Find(maze_name).transform;
        GameObject[] ghost_walls = GetChildGameObjectsWithTag(maze_transform, "GhostWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] invisi_walls = GetChildGameObjectsWithTag(maze_transform, "InvisiWall"); // Replace "ChildObject" with the name of the child object

        foreach (GameObject ghost_wall in ghost_walls)
        {
            if (ghost_wall != null)
            {
               ghost_wall.gameObject.SetActive(false);
            }
        }

        foreach (GameObject invisi_wall in invisi_walls)
        {
          if (invisi_wall != null)
            {
             invisi_wall.gameObject.SetActive(false);
            }
        }

            // make sure contra audio walls are off
        GameObject[] ghost_sound_walls = GetChildGameObjectsWithTag(maze_transform, "GhostSoundWall"); // Replace "ChildObject" with the name of the child object
        GameObject[] mute_walls =  GetChildGameObjectsWithTag(maze_transform, "MuteWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject mute_wall in mute_walls)
        {
            if (mute_wall != null)
            {
               mute_wall.gameObject.SetActive(false);
            }
        }
        

        foreach (GameObject ghost_sound_wall in ghost_sound_walls)
        {
            if (ghost_sound_wall != null)
            {
               ghost_sound_wall.gameObject.SetActive(false);
            }
        }


        
        GameObject[] morph_walls = GetChildGameObjectsWithTag(maze_transform, "MorphWall"); // Replace "ChildObject" with the name of the child object
        
        foreach (GameObject morph_wall in morph_walls)
        {
        if (morph_wall != null)
            {
            morph_wall.gameObject.SetActive(true);
            }
        }

        GameObject[] phantom_mazes = GetChildGameObjectsWithTag(maze_transform, "PhantomMazeAudio"); // Replace "ChildObject" with the name of the child object
        foreach (GameObject phantom_maze in phantom_mazes)
        {
        if (phantom_maze != null)
            {
            phantom_maze.gameObject.SetActive(true);
            }
        }
        


        // Make sure Obstruction of view is off
        ObscureObj.SetActive(false);
        // Make sure listener is ON
        audioSource3.volume = 0.5f;
        //AudioListener.volume = 1f;
         // Get the desired layer by name
        int layer = LayerMask.NameToLayer("Ignore Raycast");

        // Iterate through each child object
        foreach (Transform child in maze_transform)
        {
            if (child.CompareTag("PhantomMazeAudio"))
            {
                continue;
            }
            // Set the layer of the child object
            child.gameObject.layer = layer;
        }
        print("const_audio_clash2");

    }  
    public GameObject[] GetChildGameObjectsWithTag(Transform parent, string tag)
    {
        // Create a list to store the filtered game objects
        List<GameObject> filteredObjects = new List<GameObject>();

        // Iterate through the parent's child objects
        foreach (Transform child in parent)
        {
            // Check if the child object has the target tag
            if (child.CompareTag(tag))
            {
                // Add the child object to the filteredObjects list
                filteredObjects.Add(child.gameObject);
            }
        }

        // Convert the list to an array and return it
        return filteredObjects.ToArray();
    }
     
    
    
}

