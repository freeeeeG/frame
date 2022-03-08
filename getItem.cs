using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getItem : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> objects = new List<GameObject>();
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            col.gameObject.SetActive(false);
            objects.Add(col.gameObject);
        }
    }
}
