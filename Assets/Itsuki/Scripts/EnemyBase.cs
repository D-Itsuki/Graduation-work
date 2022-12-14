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
    ///敵毎に死んだ時の処理を実装する 
    /// </summary>
    public abstract void Dead();
}
