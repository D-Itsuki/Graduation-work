using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Presenter : MonoBehaviour
{
    // Views
    // uGUI�R���|�[�l���g���_�C���N�g�ɎQ��
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Text _hpText;
    [SerializeField] private Slider _attackSlider;
    [SerializeField] private Text _attackText;

    // Model
    [SerializeField] private PlayerModel _model;

    private void Start()
    {
        // Model -> View
        _model.HP.Subscribe(v =>
        {
                // Slider
                _hpSlider.value = v / _model.MaxHP;

                // Text
                _hpText.text = $"HP : {v.ToString("f1")}";
        })
            .AddTo(this);

        // View -> Model
        _hpSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                    // Slider�� 0.0~1.0 �Ȃ̂ŕ␳
                    // ��������Model-View�Ԃł̒l�͈͕̔␳��
                    // Presenter�̐Ӗ�
                    var value = _model.MaxHP * x;

                    // Model�ɔ��f
                    _model.SetHP(value);
            })
            .AddTo(this);

        _model.Attack.Subscribe(v =>
        {
            // Slider
            _attackSlider.value = v / _model.MaxAttack;

            // Text
            _attackText.text = $"Attack : {v.ToString("f1")}";
        })
           .AddTo(this);

        // View -> Model
        _attackSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                // Slider�� 0.0~1.0 �Ȃ̂ŕ␳
                // ��������Model-View�Ԃł̒l�͈͕̔␳��
                // Presenter�̐Ӗ�
                var value = _model.MaxAttack * x;

                // Model�ɔ��f
                _model.SetAttack(value);
            })
            .AddTo(this);
    }
}
