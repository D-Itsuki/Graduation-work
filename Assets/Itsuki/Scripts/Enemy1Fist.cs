using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Fist : EnemyState<Enemy1Fist>
{
    [SerializeField] Transform player;
    [SerializeField] float Attack1Speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 startPos;

    public Enemy1Fist(Enemy1Fist owner) : base(owner)
    {
    }

    private EnemyState<Enemy1Fist> state; // �X�e�[�g�}�V��

    enum States
    {
        Idle,
        Attack1,
        RestPos,
        Stun,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new EnemyState<Enemy1Fist>(this);//�e�X�e�[�g�̓o�^
        state.Add<StateIdle>((int)States.Idle);
        state.Add<StateAttack1>((int)States.Attack1);
        state.Add<StateResetPos>((int)States.RestPos);

        state.OnStart((int)States.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        state.OnUpdate();
    }

    private class StateIdle : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("Idle in");
        }

        public override void OnUpdate()
        {
            Debug.Log("Idling");
        }
    }

    private class StateAttack1 : StateBase
    {
        Vector2 vec;
        public override void OnStart()
        {
            Owner.player = GameObject.Find("Player").gameObject.transform; //�v���C���[�ߑ�
            vec = Owner.player.transform.position - Owner.transform.position;
            vec.Normalize();
        }

        public override void OnUpdate()
        {
            Owner.rb.velocity = vec * Owner.Attack1Speed;
        }

        
    }

    private class StateResetPos : StateBase
    {
        float resetTime = 2; //�|�W�V�������Z�b�g�ɂ����鎞��
        float elapsedTime = 0;
        float rate;

        public override void OnStart()
        {
            Debug.Log("ResetPos");
        }

        public override void OnUpdate()
        {
            elapsedTime += Time.deltaTime;  //�o�ߎ��Ԃ̉��Z
            rate = Mathf.Clamp01(elapsedTime / resetTime);

            Owner.transform.localPosition = Vector2.Lerp(Owner.transform.localPosition, Owner.startPos, rate);
        }

        public override void OnEnd()
        {
            
        }
    }

    /// <summary>
    /// �v���C���[�Ɍ������ēːi����U��
    /// </summary>
    public void Attack1()
    {
        state.ChangeState((int)States.Attack1);
    }

    public void ResetPos()
    {
        state.ChangeState((int)States.RestPos);
    }
}
