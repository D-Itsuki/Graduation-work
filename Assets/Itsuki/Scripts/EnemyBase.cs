using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private float hp;
    public float Hp
    {
        set
        {
            if (value <= 0)
            {
                hp = hp;
            }
            else
            {
                hp = value;
            }
        }

        get { return hp; }
    }

    public void Dmage(float atk)
    {
        if (hp <= 0)
            return;

        hp -= atk;

        if (hp < 1)
        {
            Dead();
        }
    }

    /// <summary>
    ///“G–ˆ‚É€‚ñ‚¾‚Ìˆ—‚ğÀ‘•‚·‚é 
    /// </summary>
    public abstract void Dead();
}
