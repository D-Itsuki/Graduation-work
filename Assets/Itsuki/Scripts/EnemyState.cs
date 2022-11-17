using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState<TOwner> : EnemyBase
{
    /// <summary>
    /// �G�̃X�e�[�g�����ׂ̊��N���X�B�G�͂�����p�����Ă���A�e�X�e�[�g�̏�����StateMachine�ɓo�^����
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>

    /// <summary>
    /// �X�e�[�g�̊��N���X�B�e�X�e�[�g�͊֐����I�[�o�[���C�h���ď�����ǉ�����
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
    private StateBase currentState; // ���݂̃X�e�[�g
    private StateBase prevState;    // �O�̃X�e�[�g
    private readonly Dictionary<int, StateBase> states = new Dictionary<int, StateBase>(); // �S�ẴX�e�[�g��`

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="owner">StateMachine���g�p����Owner</param>
    public EnemyState(TOwner owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// �X�e�[�g��`�o�^
    /// �X�e�[�g�}�V����������ɂ��̃��\�b�h���Ă�
    /// </summary>
    /// <param name="stateId">�X�e�[�gID</param>
    /// <typeparam name="T">�X�e�[�g�^</typeparam>
    public void Add<T>(int stateId) where T : StateBase, new()
    {
        if (states.ContainsKey(stateId))
        {
            Debug.LogError("���ɓo�^����Ă��܂�!! : " + stateId);
            return;
        }
        // �X�e�[�g��`��o�^
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
    /// �X�e�[�g�J�n����
    /// </summary>
    /// <param name="stateId">�X�e�[�gID</param>
    public void OnStart(int stateId)
    {
        if (!states.TryGetValue(stateId, out var nextState))
        {
            Debug.LogError("�X�e�[�gID�����݂��܂���!! : " + stateId);
            return;
        }
        // ���݂̃X�e�[�g�ɐݒ肵�ď������J�n
        currentState = nextState;
        currentState.OnStart();
    }

    /// <summary>
    /// �X�e�[�g�X�V����
    /// </summary>
    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    /// <summary>
    /// ���̃X�e�[�g�ɐ؂�ւ���
    /// </summary>
    /// <param name="stateId">�؂�ւ���X�e�[�gID</param>
    public void ChangeState(int stateId)
    {
        if (!states.TryGetValue(stateId, out var nextState))
        {
            Debug.LogError("�X�e�[�gID�����݂��܂���!! : " + stateId);
            return;
        }
        // �O�̃X�e�[�g��ێ�
        prevState = currentState;
        // �X�e�[�g��؂�ւ���
        currentState.OnEnd();
        currentState = nextState;
        currentState.OnStart();
    }

    /// <summary>
    /// �O��̃X�e�[�g�ɐ؂�ւ���
    /// </summary>
    public void ChangePrevState()
    {
        if (prevState == null)
        {
            Debug.LogError("prevState is null!!");
            return;
        }
        // �O�̃X�e�[�g�ƌ��݂̃X�e�[�g�����ւ���
        (prevState, currentState) = (currentState, prevState);
    }

    public override void Dead()
    {}
}
