using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;
using Reflex.Attributes;
namespace SCA
{
    // View
    // View can depend on the Presenter through its interface
    // View can't depend on another View.
    // View can't depend on Use Case, Gateway
    // View can inherit Monobehaviour
    public class CountTextView : MonoBehaviour
    {
        public CountTypeReactiveProperty Type;

        [Inject]
        private ICountPresenter _presenter;
        private Text _text;
        private IReadOnlyReactiveProperty<int> _reactive_property;
        private IDisposable subscription;   // Handles which property to subscribe

        private void Start()
        {
            _text = GetComponent<Text>();

            var _reactive_property = Type.Value == CountType.A ? _presenter.CountA : _presenter.CountB;

            subscription = _reactive_property
            .Subscribe((x) =>
            {
                UpdateText(x);
            }).AddTo(this);

            Type.Subscribe((x) =>
            {
                switch (x)
                {
                    case CountType.A:
                        _reactive_property = _presenter.CountA;
                        break;
                    case CountType.B:
                        _reactive_property = _presenter.CountB;
                        break;
                }
                UpdateText(_reactive_property.Value);
                subscription.Dispose();
                subscription = _reactive_property.Subscribe((x) =>
                {
                    UpdateText(x);
                }).AddTo(this);

            }).AddTo(this);


            UpdateText(0); // Initialize
        }

        private void UpdateText(int count)
        {
            _text.text = string.Format("Count {0} = {1}", Type.ToString(), count);
        }
    }
}