using System;
using System.Collections;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI.Menu
{
    public class RegisterScreenView : MonoBehaviour
    {
        public event Action<string> OnRegistrationFinish;
        
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Button _startButton;
        [SerializeField] private CanvasGroup _wrongNameHint;
        [SerializeField] private CanvasGroup _screenCanvasGroup;
        [SerializeField] private float _hideSpeed;

        private Coroutine _wrongTextHideCoroutine;
        private ISoundService _soundService;

        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
            _startButton.onClick.AddListener(SendRegistrationFinish);
        }

        public async void ShowWrongText()
        {
            if (_wrongTextHideCoroutine != null) return;
            _wrongNameHint.alpha = 1f;
            await UniTask.Delay(2000);
            _wrongTextHideCoroutine = StartCoroutine(FadeHide(_wrongNameHint));
        }

        public void Hide()
        {
            _startButton.interactable = false;
            StartCoroutine(FadeHide(_screenCanvasGroup, () => gameObject.SetActive(false)));
        }

        private IEnumerator FadeHide(CanvasGroup canvasGroup, Action onFinish = null)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * _hideSpeed;
                yield return null;
            }
            onFinish?.Invoke();
        }

        private void SendRegistrationFinish()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnRegistrationFinish?.Invoke(_nameInputField.text);
        }

        private void OnDestroy() => _startButton.onClick.RemoveListener(SendRegistrationFinish);
    }
}