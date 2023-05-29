using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace BlackJack.Code.Services.WebRequest
{
    public class WebRequestService : IWebRequestService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Dictionary<int, Action<string>> _receiveCallbacks = new Dictionary<int, Action<string>>();
        private readonly Dictionary<int, Action> _errorCallbacks = new Dictionary<int, Action>();
        private int _uid;

        public WebRequestService(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

        public void GET(string url, Action<string> onReceive, Action onError = null)
        {
            SubscribeOnEvents(_uid, onReceive, onError);
            _coroutineRunner.StartCoroutine(SendGetRequest(_uid, url));
            _uid++;
        }

        public void POST(string url, string postData, Action<string> onReceive, Action onError = null)
        {
            SubscribeOnEvents(_uid, onReceive, onError);
            _coroutineRunner.StartCoroutine(SendPostRequest(_uid, url, postData));
            _uid++;
        }

        private IEnumerator SendGetRequest(int uniqueIndex, string url)
        {
            using UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            ReceiveRequestHandler(uniqueIndex, request);
            request.Dispose();
        }

        private IEnumerator SendPostRequest(int uniqueIndex, string url, string postData)
        {
            using UnityWebRequest request = UnityWebRequest.Post(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(postData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            ReceiveRequestHandler(uniqueIndex, request);
        }

        private void ReceiveRequestHandler(int uid, UnityWebRequest request)
        {
            _errorCallbacks.TryGetValue(uid, out Action onError);
            _receiveCallbacks.TryGetValue(uid, out Action<string> onReceive);
            if (request.result == UnityWebRequest.Result.Success)
            {
                onReceive?.Invoke(request.downloadHandler.text);
            }
            _receiveCallbacks.Remove(uid);
            _errorCallbacks.Remove(uid);
        }

        private void SubscribeOnEvents(int uniqueIndex, Action<string> onReceive, Action onError)
        {
            _receiveCallbacks.Add(uniqueIndex, onReceive);
            if (onError != null) _errorCallbacks.Add(uniqueIndex, onError);
        }
    }
}