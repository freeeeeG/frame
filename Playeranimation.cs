using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimation : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed",Mathf.Abs(rb.velocity.x));
        animator.SetFloat("speed_y", rb.velocity.y);
        animator.SetBool("ground", player.onflood);
    }
    
}
