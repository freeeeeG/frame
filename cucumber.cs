using System.Collections.Generic;
using UnityEngine;

public class cucumber : enemy
{

    public List<GameObject> answers = new List<GameObject>();

    public override void Init()
    {
        base.Init();
    }
    public void setoff()
    {
        Debug.Log("111");
        if(targetPoint.GetComponent<BOOM>()&&Vector3.Distance(targetPoint.position,transform.position)<3)
        targetPoint.GetComponent<BOOM>().turnoff();
    }
    public override void SkillActtion()
    {
        if(answers.Count<1)
        base.SkillActtion();
    }
}
