using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    private Animator animator;
    [Header("time")]
    public float startTime;
    public float waitTime;
    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;
    public float boomforce;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Boom"))
        if (Time.time > startTime + waitTime)
        {
            BOOMBOOM();
        }
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void BOOMBOOM()
    {
        Collider2D[] aroundobjects = Physics2D.OverlapCircleAll(transform.position, radius,targetLayer);
        animator.Play("BoomBoom!!");
        foreach (var item in aroundobjects) 
        {
            Vector3 pos = item.transform.position - transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((pos+Vector3.up) * boomforce,ForceMode2D.Impulse);
            if (item.CompareTag("Boom") && item.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Off"))
            { 
                item.GetComponent<BOOM>().turnon();
            }

            if (item.CompareTag("Player"))
            {
                item.GetComponent<IDamageble>().GetHit(3);
            } 
        }
    }
       
    public void finish()
    {
        gameObject.SetActive(false);
    }
    public void turnoff()
    {
        animator.Play("Off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
        Debug.Log(gameObject.tag);

    }
    public void turnon()
    {
        animator.Play("Boom");
        gameObject.layer = LayerMask.NameToLayer("Boom");
        startTime = Time.time; 
    }
    
}
