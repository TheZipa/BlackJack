using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI.Statistics
{
    public class StatisticsScreenView : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private UniWebView _webView;
        
        private ISoundService _soundService;
        private string _statisticUrl;

        private void Awake()
        {
            _exitButton.onClick.AddListener(Close);
            gameObject.SetActive(false);
        }
        
        public void Construct(ISoundService soundService, string url)
        {
            _statisticUrl = url;
            _soundService = soundService;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ShowWebView();
        }

        private void ShowWebView()
        {
            _webView.BackgroundColor = Color.clear;
            _webView.SetTransparencyClickingThroughEnabled(true);
            _webView.SetBouncesEnabled(false);
            _webView.Load(_statisticUrl);
            _webView.Show(true);
        }

        private void Close()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _webView.Hide();
            gameObject.SetActive(false);
        }

        private void OnDestroy() => _exitButton.onClick.RemoveListener(Close);
    }
}