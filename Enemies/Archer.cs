using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : enemy, IDamageble
{
    public GameObject arrow;
    List<GameObject> arrows = new List<GameObject>();
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isdead = true;
        }
        animator.SetTrigger("hit");
    }

    public override void attack_work()
    {
        if (arrows.Count <= 1)
        {
            arrows.Add(Instantiate(arrow, transform.position, transform.rotation));
        }
    }

}
