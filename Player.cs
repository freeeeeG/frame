using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IDamageble
{
    public Joystick joy;
    public Rigidbody2D rb;
    public GameObject ppe;
    public float movespeed;
    public float _movespeed;
    public float _gravity;
    public Vector2 inputs;

    public int operateValue = 0;

    public Animator animator;

    [Header("skills")]

    [Header("check")]
    public Transform groundcheck;
    public float checkradius;
    public LayerMask groudlayer;
    [Header("states check")]
    public bool onflood;
    public bool onWall;

    public int _jumpNum = 1;

    int jumpNum;

    [Header("FX")]
    public GameObject jumpFX;
    public GameObject fallFX;
    [Header("Boom")]
    public GameObject Boom;
    public float CD;
    public float nextAttack;
    public int Boomnum;
    [Header("playerstate")]
    public float health;
    public bool isDead = false;
    [Header("sprint")]
    public float sprintTime = 1f;
    public float sprintCD = 3f;
    public float sprintCDTime = 0;
    public float sprintSpeed = 10f;
    public int sprint_Time = 2;
    public List<float> sprintCDTimes;
    public bool move_flag = true;

    public bool onWake = false;

    public bool onClimb = false;
    public float h2g;
    public List<baseSkill> skills_1;

    public List<baseSkill> skills_2;
    public List<baseSkill> skills_3;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _movespeed = movespeed;
        ppe.SetActive(false);
    }
    private void Update()
    {
        animator.SetBool("isdead", isDead);
        animator.SetBool("climb", onClimb);
        if (isDead)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        Sprint();
        if (move_flag)
            Operate();
        if (onWall)
        {
            rb.gravityScale = 0;
            if (onClimb)
            {
                rb.velocity = new Vector2(0, movespeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        else
        {
            rb.gravityScale = 1.5f;
        }

    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        physicalcheck();
    }


    void Operate()
    {

        if(operateValue!=0)
        {
            // rb.velocity = Vector2.zero;
            inputs = Vector2.zero;
        }
        if (operateValue == 0)
        {
            Operate_0();
            Operate_1();
            Operate_2();
            Operate_3();
        }
        else if (operateValue == 1)
        {
            Operate_1();
            Operate_2();
            Operate_3();
        }
        else if (operateValue == 2)
        {
            Operate_2();
            Operate_3();
        }
        else
        {
            Operate_3();
        }
    }
    // 能被打断的动作
    void Operate_0()
    {

        move();
        if(Input.GetButtonDown("Jump")) 
        jump();
        Squat();
    }

    void Operate_1()
    {
        Attack();
        if(skills_1!=null)
        foreach (var item in skills_1)
        {

            if (Input.GetKeyDown(item.key) && item.CDUse() && item.Flag)
            {
                item.GetComponent<baseSkill>().SkillStart(this);
                item.GetComponent<baseSkill>().Skill(this);
            }
        }
    }

    void Operate_2()
    {
        if(skills_2!=null)
        foreach (var item in skills_2)
        {
            if (Input.GetKeyDown(item.key) && item.CDUse() && item.Flag)
            {
                item.GetComponent<baseSkill>().SkillStart(this);
                item.GetComponent<baseSkill>().Skill(this);
            }
        }
    }

    void Operate_3()
    {
        if(skills_3!=null)
        foreach (var item in skills_3)
        {

            if (Input.GetKeyDown(item.key) && item.CDUse() && item.Flag)
            {
                item.GetComponent<baseSkill>().SkillStart(this);
                item.GetComponent<baseSkill>().Skill(this);
            }
        }
    }



    // TODO:蹲下
    void Squat()
    {

    }
    public void Sprint()
    {
        for (int i = 0; i < sprintCDTimes.Count; i++)
        {

            if ( Time.time > sprintCDTimes[i] && sprint_Time > 0)
            {
                sprintCDTimes[i] = Time.time + sprintCD;
                sprint_Time--;
                onWall = false;
                move_flag = false;
                rb.gravityScale = 0;
                if (inputs == Vector2.zero)
                {
                    rb.velocity = 3 * new Vector2(transform.localScale.x, 0) * movespeed;
                    //Debug.Log(transform.localScale.x);
                    movespeed = sprintSpeed;
                    StartCoroutine(StartSprint(sprintTime));
                    ppe.SetActive(true);
                    break;
                }
                else
                {
                    rb.velocity = 3 * movespeed * inputs;
                    movespeed = sprintSpeed;
                    StartCoroutine(StartSprint(sprintTime));
                    ppe.SetActive(true);
                    break;
                }

            }


        }

    }
    IEnumerator StartSprint(float time)
    {

        yield return new WaitForSeconds(time);
        movespeed = _movespeed;
        rb.gravityScale = _gravity;
        ppe.SetActive(false);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 3);
        move_flag = true;
        yield return null;

    }
    public void jump()
    {
        if (onflood)
        {
            rb.velocity = new Vector2(rb.velocity.x, movespeed);
            onflood = false;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position-new Vector3(0,3.38f,0);
            jumpNum--;

        }
        else if ( jumpNum > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, movespeed);
            jumpNum--;
            onWall = false;

        }
    }
    public void move()
    {


        float key = Input.GetAxis("Vertical");
        key = joy.Vertical;
        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalInput = joy.Horizontal;

        float kkk =  joy.Horizontal;
        // onWall = false;
        float k = Mathf.Sqrt(key * key + horizontalInput * horizontalInput);
        if (k != 0)
        {
            inputs.y = key / k;
            inputs.x = horizontalInput / k;
        }
        else if (!onWall)
        {
            inputs = Vector2.zero;
        }
        if (onflood)
        {
            rb.velocity = new Vector2(horizontalInput * movespeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(inputs.x * movespeed, rb.velocity.y);
        }

        if (kkk > 0)
            transform.localScale = new Vector3(1,1,1);
        else if (kkk < 0)
            transform.localScale = new Vector3(-1,1,1);


    }
    void physicalcheck()
    {
        onflood = Physics2D.OverlapCircle(groundcheck.position, checkradius, groudlayer);
        if (onflood)
        {
            jumpNum = _jumpNum;
            sprint_Time = 2;
        }
        var ray = Physics2D.Raycast(groundcheck.position, -Vector2.up,20, groudlayer);
        h2g = ray.distance;
    }
    void falldownFX()
    {
        fallFX.SetActive(true);
        fallFX.transform.position = transform.position + new Vector3(0, -3.37f, 0);
    }

    public void Attack()
    {
        if (!Input.GetKeyDown(KeyCode.J) || Boomnum <= 0) return;
        if (Time.time > nextAttack)
        {
            Instantiate(Boom, transform.position, Quaternion.identity);
            nextAttack = Time.time + CD;
            Boomnum--;
        }
    }

    public void GetHit(float damage)
    {
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("hit"))
        {
            health -= damage;
            Debug.Log("hoaye");
        }
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        animator.SetTrigger("hit");
        UIManager.instance.healthupdate(health);
    }
}
