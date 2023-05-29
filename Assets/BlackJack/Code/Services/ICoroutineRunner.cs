using System.Collections;
using UnityEngine;

namespace BlackJack.Code.Services
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}