using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public void gamestart()
    {
        gameObject.SetActive(true);
    }
    public void Finish()    
    {
        gameObject.SetActive(false);
    }
}
