using System.Collections;
using UnityEngine;

public class Skill_1 : baseSkill
{


    public override void Skill(Player player)
    {
            player.animator.SetBool("skill1",true);
            StartCoroutine(jump(player));
            jump(player);
            player._gravity = 0 ;
            player.rb.velocity =new Vector2(0,2*player._movespeed);
            Debug.Log(player.rb.velocity.y);
    }
    IEnumerator jump(Player player)
    {

        yield return new WaitForSeconds(0.3f);
            player.rb.velocity = new Vector2(0,-1*player._movespeed);
            player._gravity = 1.5f;
            player.animator.SetBool("skill1",false);
            
        yield return null;
    }

}
