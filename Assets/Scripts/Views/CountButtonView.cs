using UnityEngine;
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
    public class CountButtonView : MonoBehaviour
    {
        public CountTypeReactiveProperty Type;

        [Inject]
        private ICountPresenter _presenter;
        private Button _button;
        private Text _text;

        private void Start()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<Text>();
         
            _button.onClick.AddListener(() => {
                _presenter.IncrementCount(Type.Value);
            });

            Type.Subscribe((x) =>
            {
                UpdateText(x);
            }).AddTo(this);

            UpdateText(Type.Value); // Initialize
        }

        private void UpdateText(CountType type)
        {
            _text.text = string.Format("{0}++", Type);
        }
    }
}