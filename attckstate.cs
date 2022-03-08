using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attckstate : enemybasestate
{
    bool flag = false;
    public override void EnterState(enemy enemy)
    {
        enemy.animationstate = 2;
        enemy.targetPoint = enemy.attackList[0];
        flag = false;
    }

    public override void ExitState(enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate(enemy enemy)
    {
        if (enemy.attackList.Count == 0)
        {
            enemy.TransitonToState(enemy.patrolstate);
        }
        else
        {
            foreach (var item in enemy.attackList)
            {
                if (Mathf.Abs(enemy.transform.position.x - item.position.x)
                    < Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                    enemy.targetPoint = item;
            }
            if (!flag)
            {
                foreach (var item in enemy.attackList)
                {
                    enemy.targetPoint = item;
                }
            }
        }
        if (enemy.targetPoint.CompareTag("Player"))
        {
            enemy.attack();
        }
        enemy.move();
    }
}
