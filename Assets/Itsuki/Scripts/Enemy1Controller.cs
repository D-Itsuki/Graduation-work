using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyState<Enemy1Controller>
{
    private EnemyState<Enemy1Controller> state; // �X�e�[�g�}�V��

    [SerializeField] private Enemy1Fist fistM; // ���@��_�̌�
    [SerializeField] private Enemy1Fist fistP; // ������_�̌�

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
        state = new EnemyState<Enemy1Controller>(this);//�e�X�e�[�g�̓o�^
        state.Add<StateIdle>((int)States.Idle);
        state.Add<StateStun>((int)States.Stun);

        state.OnStart((int)States.Idle);
    }

    void Update()
    {
        state.OnUpdate();
    }

    public override void Dead()
    {
        Debug.Log("����");
    }

    private class StateIdle : StateBase
    {
        float stateChangeTime = 3; //��
        float time = 0;

        public override void OnStart()
        {
            Debug.Log("Idle Started");
        }

        public override void OnUpdate()
        {
            Debug.Log("Idle");
            time += Time.deltaTime;
            if (time > stateChangeTime)
            {
                Owner.fistM.Attack1();
                Owner.state.ChangeState((int)States.Stun);
            }
        }

        public override void OnEnd()
        {
            Debug.Log("Idle End");
        }
    }

    private class StateStun : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("Stun in");
        }

        public override void OnUpdate()
        {
            Debug.Log("Stunning");
        }
    }
}