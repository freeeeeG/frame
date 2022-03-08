using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour ,IDamageble,IDshake
{
    private GameObject signGameObject;
    enemybasestate nowstate;//调用状态机     
    public Animator animator;//动画机
    public int animationstate;//更改动画状态
    public bool isboss;
    [Header("movement")]
    public float speed;
    public Transform targetPoint;//目标点
    public Rigidbody2D rb;
    public Transform pointa, pointb;//巡逻点
    [Header("attack setting")]
    public float nextattack = 0;
    public float nextskill = 0;
    public float attackrate, skillattackrate;
    public float attackarea, skillattackarea;
    [Header("Base State")]
    public float health;
    public bool isdead;
    [Header("state")]
    public List<Transform> attackList = new List<Transform>();//储存需要追寻的目标
    public bool def_flag = false;
    public patrolstate patrolstate = new patrolstate();//巡逻模式
    public attckstate attckstate = new attckstate();//攻击模式
    public defstate defstate = new defstate();//防御模式
    public enum attackmode
    {
        far_attack,
        min_attack,
        near_attack
    };
    public attackmode att_mode;
    public virtual void Init()//初始化
    {
        signGameObject = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        targetPoint = pointa;
    }
    private void awake()
    {
        Init();
    }
    void Start()
    {
        targetPoint = pointa;
        TransitonToState(patrolstate);//巡逻状态
        if (isboss)
            UIManager.instance.SetbossHealth(health);
    }

    void Update()
    {
        animator.SetBool("isdead", isdead);

        if (isboss)
            UIManager.instance.updateBossHealth(health);
        if (isdead)
        {
            return;
        }
        nowstate.OnUpdate(this);
        animator.SetInteger("state", animationstate);
    }

    public void TransitonToState(enemybasestate state)
    {
        nowstate = state;
        nowstate.EnterState(this);
    }
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
    public void shake(Transform pos)
    {
        Debug.Log("Shake");
        rb.AddForce(pos.position-transform.position,ForceMode2D.Impulse);
    }

    //移动相关代码
    public void move()//走向目标移动
    {

        switch (att_mode)
        {
            case attackmode.near_attack:
                transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
                FlieDirection();
                break;
            case attackmode.far_attack:
                if (targetPoint != pointa && targetPoint != pointb)
                {

                    if (targetPoint.position.x > transform.position.x)
                        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position + new Vector3(-6.0f, 0), speed * Time.deltaTime);
                    else if (targetPoint.position.x <= transform.position.x)
                        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position + new Vector3(6.0f, 0), speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

                }
                FlieDirection();
                break;
        }
    }
    public void FlieDirection()//面向朝向

    {
        if (targetPoint.position.x > transform.position.x)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void switchpoint()//选择巡逻目标点
    {
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.1)
        {
            animationstate = 0;
            if (targetPoint == pointa)
                targetPoint = pointb;
            else
                targetPoint = pointa;
        }

    }
    public void attack()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackarea)
        {
            if (Time.time > nextattack)
            {
                animator.SetTrigger("attack");
                attack_work();
                nextattack = Time.time + attackrate;
            }
        }
    }

    public virtual void attack_work()
    {

    }
    public void OnTriggerStay2D(Collider2D boy)
    {
        if (!attackList.Contains(boy.transform) && !GameManager.instance.gameover && boy.CompareTag("Player"))
            attackList.Add(boy.transform);
    }
    public void OnTriggerExit2D(Collider2D boy)
    {
        attackList.Remove(boy.transform);
    }

    public virtual void SkillActtion()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillattackarea)
        {
            if (Time.time > nextskill)
            {
                animator.SetTrigger("skill");
                nextskill = Time.time + skillattackrate;
            }

        }

    }
    public void cheakanimation()
    {
        animationstate = 1;
    }


    public void attackareastart()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void attckareaend()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isdead && !GameManager.instance.gameover && collision.CompareTag("Player"))
            StartCoroutine(OnAlarm());
    }

    IEnumerator OnAlarm()
    {
        signGameObject.SetActive(true);
        yield return new WaitForSeconds(signGameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        signGameObject.SetActive(false);
    }


}






