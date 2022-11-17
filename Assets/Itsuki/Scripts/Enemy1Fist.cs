using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Fist : EnemyState<Enemy1Fist>
{
    [SerializeField] Transform player;
    [SerializeField] float Attack1Speed;
    [SerializeField] Rigidbody2D rb;

    public Enemy1Fist(Enemy1Fist owner) : base(owner)
    {
    }

    private EnemyState<Enemy1Fist> state; // ステートマシン

    enum States
    {
        Idle,
        Attack1,
        Stun,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new EnemyState<Enemy1Fist>(this);//各ステートの登録
        state.Add<StateIdle>((int)States.Idle);
        state.Add<StateAttack1>((int)States.Attack1);

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
            vec = Owner.player.transform.position - Owner.transform.position;
            vec.Normalize();
        }

        public override void OnUpdate()
        {
            Owner.rb.velocity = vec * Owner.Attack1Speed;
        }
    }

    /// <summary>
    /// プレイヤーに向かって突進する攻撃
    /// </summary>
    public void Attack1()
    {
        state.ChangeState((int)States.Attack1);
    }
}
