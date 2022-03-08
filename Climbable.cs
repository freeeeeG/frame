using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Climbable : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other);

        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.C))
            {
                other.GetComponent<Player>().onWall = true;
            }
            else
            {
                other.GetComponent<Player>().onWall = false;
            }
            if (Input.GetKey(KeyCode.W))
            {
                other.GetComponent<Player>().onClimb = true;
                other.GetComponent<Player>().move_flag = false;
            }
            else
            {
                other.GetComponent<Player>().onClimb = false;
                other.GetComponent<Player>().move_flag = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().onWall = false;
        }
    }


}