using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Fist : EnemyState<Enemy1Fist>, IDamageble
{
    [SerializeField] Transform player;
    [SerializeField] float Attack1Speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float atk;
    [SerializeField] Enemy1Controller enemyBody;

    int hpTemp = 0;

    [Header("敵本体からのLocalPosition")]
    [SerializeField] Vector2 startPos;

    public Enemy1Fist(Enemy1Fist owner) : base(owner)
    {
    }

    private EnemyState<Enemy1Fist> state; // ステートマシン

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
        hpTemp = (int)Hp;
        state = new EnemyState<Enemy1Fist>(this);//各ステートの登録
        state.Add<StateIdle>((int)States.Idle);
        state.Add<StateAttack1>((int)States.Attack1);
        state.Add<StateResetPos>((int)States.RestPos);
        state.Add<StateStun>((int)States.Stun);

        state.OnStart((int)States.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        state.OnUpdate();
    }

    private class StateIdle : StateBase
    {
        Vector2 vec;
        public override void OnStart()
        {
            //Debug.Log("Idle in");
            Owner.rb.velocity = Vector2.zero;
        }

        public override void OnUpdate()
        {
            //Debug.Log("Idling");
            Owner.player = GameObject.Find("Player").gameObject.transform; //プレイヤー捕捉
            vec = Owner.player.transform.position - Owner.transform.position;
            vec.Normalize();
            Owner.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        }
    }

    private class StateAttack1 : StateBase
    {
        Vector2 vec;
        public override void OnStart()
        {
            Owner.player = GameObject.Find("Player").gameObject.transform; //プレイヤー捕捉
            vec = Owner.player.transform.position - Owner.transform.position;
            vec.Normalize();
            Owner.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
            Owner.rb.velocity = vec * Owner.Attack1Speed;
        }
        
    }

    private class StateResetPos : StateBase
    {
        float resetTime = 2; //ポジションリセットにかける時間。仮
        float elapsedTime = 0;
        float rate;

        public override void OnStart()
        {
            Debug.Log("ResetPos");
            elapsedTime = 0;
        }

        public override void OnUpdate()
        {
            elapsedTime += Time.deltaTime;  //経過時間の加算
            rate = Mathf.Clamp01(elapsedTime / resetTime);

            Owner.transform.localPosition = Vector2.Lerp(Owner.transform.localPosition, Owner.startPos, rate);
            if (elapsedTime >= resetTime)
            {
                Owner.state.ChangeState((int)States.Idle);
            }
        }

        public override void OnEnd()
        {
            
        }
    }

    private class StateStun : StateBase
    {
        float timer = 5;//仮
        float temp = 0;

        public override void OnStart()
        {
            Debug.Log("Stun in");
            temp = 0;
        }

        public override void OnUpdate()
        {
            Debug.Log(temp);
            temp += Time.deltaTime;
            if (temp > timer)
            {
                Owner.state.ChangeState((int)States.Idle);
            }
        }

        public override void OnEnd()
        {
            Owner.Hp = Owner.hpTemp;
        }
    }

    /// <summary>
    /// プレイヤーに向かって突進する攻撃
    /// </summary>
    public void Attack1()
    {
        state.ChangeState((int)States.Attack1);
    }

    public void ResetPos()
    {
        state.ChangeState((int)States.RestPos);
    }

    public void Damage(float damage)
    {
        Debug.Log("Damaged : atk " + damage);
        if (Hp <= 0)
            return;

        Hp -= damage;

        if (Hp < 1)
            Dead();
    }

    public override void Dead()
    {
        enemyBody.Stun();
        state.ChangeState((int)States.Stun);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var temp = collision.gameObject.GetComponent<IDamageble>();

        if (temp != null)
            temp.Damage(atk);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}
