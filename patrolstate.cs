public class patrolstate : enemybasestate
{
    public override void EnterState(enemy enemy)
    {

        enemy.animationstate = 0;
        enemy.Init();
    }

    public override void ExitState(enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate(enemy enemy)
    {
        enemy.switchpoint();
        if (!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.animationstate = 1;
            enemy.move();
            enemy.switchpoint();
        }

        if (enemy.attackList.Count > 0 && !enemy.def_flag)
            enemy.TransitonToState(enemy.attckstate);
        else if (enemy.attackList.Count > 0 && enemy.def_flag)
            enemy.TransitonToState(enemy.defstate);
    }
}
