using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState<TOwner> : EnemyBase
{
    /// <summary>
    /// 敵のステートを作る為の基底クラス。敵はこれを継承してつくり、各ステートの処理をStateMachineに登録する
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>

    /// <summary>
    /// ステートの基底クラス。各ステートは関数をオーバーライドして処理を追加する
    /// </summary>
    public abstract class StateBase
    {
        public EnemyState<TOwner> StateMachine;
        protected TOwner Owner => StateMachine.Owner;

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnd() { }
    }

    private TOwner Owner { get; }
    private StateBase currentState; // 現在のステート
    private StateBase prevState;    // 前のステート
    private readonly Dictionary<int, StateBase> states = new Dictionary<int, StateBase>(); // 全てのステート定義

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="owner">StateMachineを使用するOwner</param>
    public EnemyState(TOwner owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// ステート定義登録
    /// ステートマシン初期化後にこのメソッドを呼ぶ
    /// </summary>
    /// <param name="stateId">ステートID</param>
    /// <typeparam name="T">ステート型</typeparam>
    public void Add<T>(int stateId) where T : StateBase, new()
    {
        if (states.ContainsKey(stateId))
        {
            Debug.LogError("既に登録されています!! : " + stateId);
            return;
        }
        // ステート定義を登録
        var newState = new T
        {
            StateMachine = this
        };
        states.Add(stateId, newState);
    }

    public StateBase GetCurrentState()
    {
        return currentState;
    }

    /// <summary>
    /// ステート開始処理
    /// </summary>
    /// <param name="stateId">ステートID</param>
    public void OnStart(int stateId)
    {
        if (!states.TryGetValue(stateId, out var nextState))
        {
            Debug.LogError("ステートIDが存在しません!! : " + stateId);
            return;
        }
        // 現在のステートに設定して処理を開始
        currentState = nextState;
        currentState.OnStart();
    }

    /// <summary>
    /// ステート更新処理
    /// </summary>
    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    /// <summary>
    /// 次のステートに切り替える
    /// </summary>
    /// <param name="stateId">切り替えるステートID</param>
    public void ChangeState(int stateId)
    {
        if (!states.TryGetValue(stateId, out var nextState))
        {
            Debug.LogError("ステートIDが存在しません!! : " + stateId);
            return;
        }
        // 前のステートを保持
        prevState = currentState;
        // ステートを切り替える
        currentState.OnEnd();
        currentState = nextState;
        currentState.OnStart();
    }

    /// <summary>
    /// 前回のステートに切り替える
    /// </summary>
    public void ChangePrevState()
    {
        if (prevState == null)
        {
            Debug.LogError("prevState is null!!");
            return;
        }
        // 前のステートと現在のステートを入れ替える
        (prevState, currentState) = (currentState, prevState);
    }

    public override void Dead()
    {}
}
