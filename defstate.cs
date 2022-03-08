using System.Collections;
using UnityEngine;
public class defstate : enemybasestate
{
    public override void EnterState(enemy enemy)
    {
        enemy.animationstate = 4;//TODO: 防御动画
        enemy.targetPoint = enemy.attackList[0];
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
        }
        attack(enemy);
        enemy.move();
    }
    public override void ExitState(enemy enemy)
    {

    }

    IEnumerator attack(enemy enemy)
    {
        yield return new WaitForSeconds(2);
        if (enemy.attackList.Count == 0)
            enemy.TransitonToState(enemy.patrolstate);

    }
}
