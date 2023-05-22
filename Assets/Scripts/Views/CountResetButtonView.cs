using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Reflex.Attributes;
using TMPro;

namespace SCA
{
     // View
    // View can depend on the Presenter through its interface
    // View can't depend on another View.
    // View can't depend on Use Case, Gateway
    // View can inherit Monobehaviour
    public class CountResetButtonView : MonoBehaviour
    {
        public CountTypeReactiveProperty Type; // ReactiveProperty is a kind of Observable. You can subscribe to it.
        [Inject]
        private ICountPresenter _presenter; // Inject interfaces, not concrete classes
        private Button _button;
        private TMP_Text _text;
        private void Start()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();

            _button.onClick.AddListener(() => {
                _presenter.ResetCount(Type.Value);
            });

            Type.Subscribe((x) =>
            {
                UpdateText(x);
            }).AddTo(this);

            UpdateText(Type.Value); // Initialize
        }

        private void UpdateText(CountType type)
        {
            _text.text = string.Format("Reset {0}", Type);
        }
    }

}
