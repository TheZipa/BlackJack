using BlackJack.Code.Data.Progress;
using BlackJack.Code.Extensions;
using BlackJack.Code.Services.PersistentProgress;
using UnityEngine;

namespace BlackJack.Code.Services.SaveLoad
{
    public class PrefsSaveLoad : ISaveLoad
    {
        private readonly IPersistentProgress _playerProgress;
        private const string ProgressKey = "Progress";

        public PrefsSaveLoad(IPersistentProgress playerProgress) => _playerProgress = playerProgress;
        
        public void SaveProgress() => 
            PlayerPrefs.SetString(ProgressKey, _playerProgress.Progress.ToJson());

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}