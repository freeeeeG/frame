using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chop : baseSkill
{

    int _Tricks = 3;
    public int Trick = 0;
    public List<float> SkillTimes = new List<float>();
    public List<float> SkillStayTimes = new List<float>();
    public Player player;
    public bool flag = true;


    private void Update()
    {
        // if (!player.onflood)
        // {
        //     Flag = false;
        //     Trick = 0;
        // }
        // else
        // {
        //     Flag = true;
        // }
        player.animator.SetInteger("chop", Trick);
    }
    public override void Skill(Player player)
    {
        base.Skill(player);
        if (Trick == 0 && flag)
        {
            //TODO: fri;
            flag = false;
            player.animator.SetBool("chop_idle", flag);
            StartCoroutine(waitTime(SkillTimes[0], 1));
            StartCoroutine(stayTime(SkillStayTimes[0], 1));
        }
        if (Trick == 1 && flag)
        {
            //TODO:sec;
            flag = false;
            player.animator.SetBool("chop_idle", flag);
            StartCoroutine(waitTime(SkillTimes[1], 2));
            StartCoroutine(stayTime(SkillStayTimes[1], 2));

        }
        if (Trick == 2 && flag)
        {
            flag = false;
            StartCoroutine(waitTime(SkillTimes[2], 3));

        }

    }


    IEnumerator waitTime(float time, int i)
    {

        Trick = i;
        yield return new WaitForSeconds(time);
        flag = true;
        if (Trick == 3)
        {
            Trick = 0;
            player.animator.SetBool("chop_idle", false);
        }
        else
        {
            player.animator.SetBool("chop_idle", flag);
        }
        yield return null;
    }
    IEnumerator stayTime(float time, int i)
    {

        yield return new WaitForSeconds(time);
        
        if (Trick == i)
        {
            player.animator.SetBool("chop_idle", false);
            Trick = 0;
            flag = true;
            player.animator.SetInteger("chop", 0);
        }
        Debug.Log(Trick);
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {

        }
    }
}
