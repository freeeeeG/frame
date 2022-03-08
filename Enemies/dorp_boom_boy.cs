using UnityEngine;

public class dorp_boom_boy : enemy, IDamageble
{

    public GameObject boom;
    public float boomforce;
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
        var k = Instantiate(boom, transform.position+Vector3.up, transform.rotation);
        k.GetComponent<Rigidbody2D>().AddForce((targetPoint.transform.position-Vector3.up-transform.position) * boomforce,ForceMode2D.Impulse);
    }
    





}
