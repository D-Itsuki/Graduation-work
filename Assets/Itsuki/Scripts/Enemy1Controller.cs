using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy1Controller : EnemyState<Enemy1Controller>, IDamageble
{
    private EnemyState<Enemy1Controller> state; // ステートマシン

    [SerializeField] private Enemy1Fist fistM; // 魔法弱点の拳
    [SerializeField] private Enemy1Fist fistP; // 物理弱点の拳
    [SerializeField] UnityEvent gameOverevent;

    enum States 
    { 
        Idle,
        Attack1,
        Stun,
    }

    public Enemy1Controller(Enemy1Controller owner) : base(owner)
    {
    }

    void Start()
    {
        state = new EnemyState<Enemy1Controller>(this);//各ステートの登録
        state.Add<StateIdle>((int)States.Idle);
        state.Add<StateAttack1>((int)States.Attack1);
        state.Add<StateStun>((int)States.Stun);

        state.OnStart((int)States.Idle);
    }

    void Update()
    {
        state.OnUpdate();
    }

    public override void Dead()
    {
        Debug.Log("死んだ");
        gameOverevent?.Invoke();
    }

    public void Damage(float damage)
    {
        Debug.Log("Damaged : " + damage);
        if (Hp <= 0)
        {
            return;
        }

        Hp -= damage;

        if (Hp < 1)
        {
            Dead();
        }
    }

    public void Stun()
    {
        state.ChangeState((int)States.Stun);
    }

    private class StateIdle : StateBase
    {
        float stateChangeTime = 3; //仮
        float time = 0;

        public override void OnStart()
        {
            //Debug.Log("Idle Started");
        }

        public override void OnUpdate()
        {
            //Debug.Log("Idle");
            time += Time.deltaTime;
            if (time > stateChangeTime)
            {
                Owner.state.ChangeState((int)States.Attack1);
            }
        }

        public override void OnEnd()
        {
            //Debug.Log("Idle End");
        }
    }

    private class StateAttack1 : StateBase
    {
        float attackTime = 3; //仮
        float time = 0;
        float rearGap = 6; //仮
        bool temp = true;

        public override void OnStart()
        {
            //Debug.Log("Attack1 In");
            Owner.fistM.Attack1();
        }

        public override void OnUpdate()
        {
            time += Time.deltaTime;
            if (time > attackTime && temp == true)
            {
                //Debug.Log("Punch2");
                Owner.fistP.Attack1();
                temp = false;
            }
            if (time > rearGap)
            {
                Owner.state.ChangeState((int)States.Idle);
            }
        }

        public override void OnEnd()
        {
            Owner.fistM.ResetPos();
            Owner.fistP.ResetPos();
        }
    }

    private class StateStun : StateBase
    {
        float timer = 5;//仮
        float temp = 0;

        public override void OnStart()
        {
            //Debug.Log("Stun in");
        }

        public override void OnUpdate()
        {
            Debug.Log("Stunning");
            temp += Time.deltaTime;
            if (timer > temp)
            {
                Owner.state.ChangeState((int)States.Idle);
            }
        }
    }
}