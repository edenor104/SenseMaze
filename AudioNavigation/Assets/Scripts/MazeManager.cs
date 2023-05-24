using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public Camera mainCamera; // Drag the main camera object into this field in the inspector
    //public AudioListener listenerObject;
    public GameObject ObscureObj;



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
                ApplyConditionOne(maze_name);
                break;
            case "visual_only" when true:
                ApplyConditionTwo(maze_name);
                break;
            case "contra_visual" when true:
                ApplyConditionThree(maze_name);
                break;
            case "contra_audio" when true:
                ApplyConditionFour(maze_name);
                break;
        }
    }
    

        void ApplyConditionOne(string maze_name)
        {
            
            // Condition 1 - Audio Only
            // Make contra visual walls are off
            Transform maze_transform = GameObject.Find(maze_name).transform;
            Transform ghost_wall = maze_transform.Find("GhostWall"); // Replace "ChildObject" with the name of the child object
            Transform invisi_wall = maze_transform.Find("InvisiWall"); // Replace "ChildObject" with the name of the child object
            if (invisi_wall != null)
            {
                invisi_wall.gameObject.SetActive(false);
            }

            if (ghost_wall != null)
            {
                ghost_wall.gameObject.SetActive(false);
            }


             // make sure contra audio walls are off
            Transform ghost_sound_wall = maze_transform.Find("GhostSoundWall"); // Replace "ChildObject" with the name of the child object
            Transform mute_wall = maze_transform.Find("MuteWall"); // Replace "ChildObject" with the name of the child object
            if (ghost_sound_wall != null)
            {
                ghost_sound_wall.gameObject.SetActive(false);
            }

            if (mute_wall != null)
            {
                mute_wall.gameObject.SetActive(false);
            }

            Transform morph_wall = maze_transform.Find("MorphWall"); // Replace "ChildObject" with the name of the child object
            if (morph_wall != null)
            {
                morph_wall.gameObject.SetActive(true);
            }
            
            

            print("audio");
            // Make sure Obstruction of view is on -NO VISION
            ObscureObj.SetActive(true);
            // Make sure listener is ON
            AudioListener.volume = 1f;

        }

        void ApplyConditionTwo(string maze_name)
        {
            // Condition 2 - Visual Only
            // Make contra visual walls are off
            Transform maze_transform = GameObject.Find(maze_name).transform;
            Transform ghost_wall = maze_transform.Find("GhostWall"); // Replace "ChildObject" with the name of the child object
            Transform invisi_wall = maze_transform.Find("InvisiWall"); // Replace "ChildObject" with the name of the child object
            if (invisi_wall != null)
            {
                invisi_wall.gameObject.SetActive(false);
            }

            if (ghost_wall != null)
            {
                ghost_wall.gameObject.SetActive(false);
            }


             // make sure contra audio walls are off
            Transform ghost_sound_wall = maze_transform.Find("GhostSoundWall"); // Replace "ChildObject" with the name of the child object
            Transform mute_wall = maze_transform.Find("MuteWall"); // Replace "ChildObject" with the name of the child object
            if (ghost_sound_wall != null)
            {
                ghost_sound_wall.gameObject.SetActive(false);
            }

            if (mute_wall != null)
            {
                mute_wall.gameObject.SetActive(false);
            }
            
            Transform morph_wall = maze_transform.Find("MorphWall"); // Replace "ChildObject" with the name of the child object
            if (morph_wall != null)
            {
                morph_wall.gameObject.SetActive(true);
            }


            // Make sure Obstruction of view is off
            ObscureObj.SetActive(false);
            // Make sure listener is off - NO SOUND
            AudioListener.volume = 0f;
            print("visual");

        }

        void ApplyConditionThree(string maze_name)
        {
            // Apply condition 3 - contra visual - visual is false, needs to count on Audio
            // Make sure Obstruction of view is off
            ObscureObj.SetActive(false);
            // Make sure listener is on
            AudioListener.volume = 1f;
            // Make sure contra visual walls are ON
            Transform maze_transform = GameObject.Find(maze_name).transform;
            Transform ghost_wall = maze_transform.Find("GhostWall"); // Replace "ChildObject" with the name of the child object
            Transform invisi_wall = maze_transform.Find("InvisiWall"); // Replace "ChildObject" with the name of the child object
            if(invisi_wall != null)
            {
                invisi_wall.gameObject.SetActive(true);
            }

            if(ghost_wall != null)
            {
                ghost_wall.gameObject.SetActive(true);
            }

             // make sure contra audio walls are off
            Transform ghost_sound_wall = maze_transform.Find("GhostSoundWall"); // Replace "ChildObject" with the name of the child object
            Transform mute_wall = maze_transform.Find("MuteWall"); // Replace "ChildObject" with the name of the child object

            if (ghost_sound_wall != null)
            {
                ghost_sound_wall.gameObject.SetActive(false);
            }

            if (mute_wall != null)
            {
                mute_wall.gameObject.SetActive(false);
            }
            
            Transform morph_wall = maze_transform.Find("MorphWall"); // Replace "ChildObject" with the name of the child object
            if (morph_wall != null)
            {
                morph_wall.gameObject.SetActive(false);
            }

            print("contra_visual");

        }

        void ApplyConditionFour(string maze_name)
        {
            // Apply condition 4 to the maze objects
            ObscureObj.SetActive(false);
            AudioListener.volume = 1f;
            print("contra_audio");            
            // Make sure ghost walls are off
            Transform maze_transform = GameObject.Find(maze_name).transform;
            Transform ghost_wall = maze_transform.Find("GhostWall"); // Replace "ChildObject" with the name of the child object
            Transform invisi_wall = maze_transform.Find("InvisiWall"); // Replace "ChildObject" with the name of the child object
            if (invisi_wall != null)
            {
                invisi_wall.gameObject.SetActive(false);
            }

            if (ghost_wall != null)
            {
                ghost_wall.gameObject.SetActive(false);
            }
            // make sure contra audio walls are ON
            Transform ghost_sound_wall = maze_transform.Find("GhostSoundWall"); // Replace "ChildObject" with the name of the child object
            Transform mute_wall = maze_transform.Find("MuteWall"); // Replace "ChildObject" with the name of the child object

            if (ghost_sound_wall != null)
            {
                ghost_sound_wall.gameObject.SetActive(true);
            }

            if (mute_wall != null)
            {
                mute_wall.gameObject.SetActive(true);
            }

            Transform morph_wall = maze_transform.Find("MorphWall"); // Replace "ChildObject" with the name of the child object
            if (morph_wall != null)
            {
                morph_wall.gameObject.SetActive(false);
            }
            


        }
    
}
