using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject checkObject;
    public List<GameObject> checkList;
    public bool isCheckPassed = false;
    bool isCol;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCol)
        {
            transform.GetChild(0).GetComponent<CanvasGroup>().alpha -= 0.01f ;
        }
        else
        {
            transform.GetChild(0).GetComponent<CanvasGroup>().alpha += 0.01f ;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isCheckPassed)
            {
                isCol = true;
            }
            checkList = GameObject.Find("Player").GetComponent<getItem>().objects;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (checkList.Contains(checkObject) && Input.GetKey(KeyCode.F))
            {
                GameObject.Find("Player").GetComponent<getItem>().objects.Remove(checkObject);
                transform.GetChild(0).gameObject.SetActive(false);
                isCol = false;
                isCheckPassed = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !isCheckPassed)
        {
            isCol = false;
        }
    }

    
}
