using System.Collections;
using UnityEngine;

public class longAtk : baseSkill
{
    bool flag = true;
    public float time = 0.0f;
    private void Update() {
        
    }
    public override void Skill(Player player)
    {
        flag = true;
        player.rb.velocity = new Vector3(0,player.rb.velocity.y);
        //TODO: 防御动画
        if(player.rb.velocity.x!=0)
        {
            
            flag = false;
        }
        else
        {
            atk(time);
        }

    }
    IEnumerator atk(float time)
    {
        yield return new WaitForSeconds(time);
        if(flag==true)
        {
            //TODO:atk animation
        }
    }
}
