using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChop : baseSkill
{
    public Player player;
    private void Update()
    {
        Flag = !player.onflood;
    }
    public override void Skill(Player player)
    {
        player.rb.gravityScale = 0;

        base.Skill(player);
        S();
        if (player.inputs.x > 0 && player.inputs.y == 0)
        {
            Debug.Log("右");
        }
        else if (player.inputs.x > 0 && player.inputs.y > 0)
        {
            Debug.Log("右上");
        }
        else if (player.inputs.x > 0 && player.inputs.y < 0)
        {
            Debug.Log("右下");
        }
        else if (player.inputs.x == 0 && player.inputs.y > 0)
        {
            Debug.Log("上");
        }
        else if (player.inputs.x == 0 && player.inputs.y < 0)
        {
            Debug.Log("下");
        }
        else if (player.inputs.x < 0 && player.inputs.y == 0)
        {
            Debug.Log("左");
        }
        else if (player.inputs.x < 0 && player.inputs.y > 0)
        {
            Debug.Log("左上");
        }
        else if (player.inputs.x < 0 && player.inputs.y < 0)
        {
            Debug.Log("左下");
        }
        else
        {
            Debug.Log("面朝");
        }
    }

    public void S()
    {
        player.onWall = false;
        player.move_flag = false;
        player.rb.gravityScale = 0;
        if (player.inputs == Vector2.zero)
        {
            if (player.GetComponent<SpriteRenderer>().flipX == true)
            {
                player.rb.velocity = 0.2f * new Vector2(-1, 0) * player.movespeed;
            }
            else
            {
                player.rb.velocity = 0.2f * new Vector2(1, 0) * player.movespeed;
            }
            //Debug.Log(transform.localScale.x);
            player.movespeed = player.sprintSpeed;
            StartCoroutine(StartSprint(SkillTime));
            player.ppe.SetActive(true);
        }
        else
        {
            player.rb.velocity = 0.2f * player.movespeed * player.inputs;
            player.movespeed = player.sprintSpeed;
            StartCoroutine(StartSprint(SkillTime));
            player.ppe.SetActive(true);
        }
    }
    IEnumerator StartSprint(float time)
    {

        yield return new WaitForSeconds(time);
        player.movespeed = player._movespeed;
        player.rb.gravityScale = player._gravity;
        player.ppe.SetActive(false);
        player.move_flag = true;
        yield return null;

    }
}
