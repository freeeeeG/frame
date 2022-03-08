using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class tank : enemy
{
    bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag&&attackList.Count>0)
        {
            TransitonToState(defstate);
            flag = false;
        }
    }
    
    IEnumerator changed_flag()
    {
        yield return new WaitForSeconds(2);
        flag = true;
    }
}
