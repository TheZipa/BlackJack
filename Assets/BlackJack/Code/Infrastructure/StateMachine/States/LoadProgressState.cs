using System;
using System.Threading.Tasks;
using BlackJack.Code.Data.Progress;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.SaveLoad;
using BlackJack.Code.Services.Sound;
using BlackJack.Code.Services.StaticData;
using Firebase.Database;
using Firebase.Extensions;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentProgress _playerProgress;
        private readonly ISaveLoad _saveLoadService;
        private readonly IStaticData _staticDataService;
        private readonly ISoundService _soundService;

        public LoadProgressState(IStateSwitcher stateSwitcher, IPersistentProgress playerProgress,
            ISaveLoad saveLoadService, IStaticData staticDataService, ISoundService soundService)
        {
            _staticDataService = staticDataService;
            _soundService = soundService;
            _saveLoadService = saveLoadService;
            _playerProgress = playerProgress;
            _stateSwitcher = stateSwitcher;
        }
        
        public async void Enter()
        {
            LoadProgressOrInitNew();
            await LoadTournamentUrl();
            InitializeSoundVolume();
            _stateSwitcher.SwitchTo<LoadPersistentEntityState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() =>
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress() => new PlayerProgress(_staticDataService.BlackJackSettingsConfig.StartBalance);
        
        private async Task LoadTournamentUrl() =>
            await FirebaseDatabase.DefaultInstance
                .GetReference(_staticDataService.BlackJackSettingsConfig.FirebaseKey)
                .GetValueAsync()
                .ContinueWithOnMainThread(OnDatabaseDataReceived);

        private void OnDatabaseDataReceived(Task<DataSnapshot> result)
        {
            if (result.IsCompleted == false) return;
            string tournamentUrl = result.Result.Value + "?guid=" + _playerProgress.Progress.Guid;
            TrySaveTournamentUrl(tournamentUrl);
        }

        private void TrySaveTournamentUrl(string tournamentUrl)
        {
            if (!String.IsNullOrEmpty(_playerProgress.Progress.TournamentUrl)
                && _playerProgress.Progress.TournamentUrl == tournamentUrl) return;
            _playerProgress.Progress.TournamentUrl = tournamentUrl;
            _saveLoadService.SaveProgress();
        }

        private void InitializeSoundVolume()
        {
            _soundService.Construct(_staticDataService.SoundData, _playerProgress.Progress.Settings);
            _soundService.PlayBackgroundMusic();
        }
    }
}