using System;

namespace BlackJack.Code.Services.WebRequest
{
    public interface IWebRequestService
    {
        void GET(string url, Action<string> onReceive, Action onError = null);
        void POST(string url, string postData, Action<string> onReceive, Action onError = null);
    }
}