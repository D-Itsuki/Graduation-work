using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private float hp;
    public float Hp
    {
        set { hp = value; }

        get { return hp; }
    }


    /// <summary>
    ///�G���Ɏ��񂾎��̏������������� 
    /// </summary>
    public abstract void Dead();
}
