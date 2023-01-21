using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerModel : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> HP => _hp;
    public IReadOnlyReactiveProperty<float> Attack => _attack;

    // ç≈ëÂíl
    public float MaxHP = 100f;
    public float MaxAttack = 200f;

    [SerializeField]
    private FloatReactiveProperty _hp = new FloatReactiveProperty(0);
    [SerializeField]
    private FloatReactiveProperty _attack = new FloatReactiveProperty(0);

    public void SetHP(float value)
    {
        // êîílÇÃîÕàÕÇï‚ê≥
        value = Mathf.Clamp(value, 0, MaxHP);
        _hp.Value = value;
    }

    public void SetAttack(float value)
    {
        value = Mathf.Clamp(value, 0, MaxAttack);
        _attack.Value = value;
    }

    private void OnDestroy()
    {
        _hp.Dispose();
    }
}
