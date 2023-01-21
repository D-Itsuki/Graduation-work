using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Presenter : MonoBehaviour
{
    // Views
    // uGUIコンポーネントをダイレクトに参照
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
                    // Sliderは 0.0~1.0 なので補正
                    // こういうModel-View間での値の範囲補正も
                    // Presenterの責務
                    var value = _model.MaxHP * x;

                    // Modelに反映
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
                // Sliderは 0.0~1.0 なので補正
                // こういうModel-View間での値の範囲補正も
                // Presenterの責務
                var value = _model.MaxAttack * x;

                // Modelに反映
                _model.SetAttack(value);
            })
            .AddTo(this);
    }
}
