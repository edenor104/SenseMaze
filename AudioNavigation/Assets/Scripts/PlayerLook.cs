using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook: MonoBehaviour
{
    public float rotationAngle = 45f;
    private bool flag1 = false;
    private bool flag2 = false;


    void Update()
    {
        // Rotate player left when 'RightArrow' key is pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!flag1)
            {

                transform.Rotate(0, -rotationAngle, 0, Space.Self);

                flag1 = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            flag1 = false;
        }

        // Rotate player right when 'LeftArrow' key is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!flag2)
            {

                transform.Rotate(0, rotationAngle, 0, Space.Self);
                flag2 = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            flag2 = false;
        }
        
    }
}
