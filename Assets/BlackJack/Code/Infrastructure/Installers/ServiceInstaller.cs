using BlackJack.Code.Services;
using BlackJack.Code.Services.CardMove;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.Factories.GameFactory;
using BlackJack.Code.Services.Factories.PersistentEntityFactory;
using BlackJack.Code.Services.Factories.UIFactory;
using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.SaveLoad;
using BlackJack.Code.Services.SceneLoader;
using BlackJack.Code.Services.Sound;
using BlackJack.Code.Services.StaticData;
using BlackJack.Code.Services.StaticData.StaticDataProvider;
using BlackJack.Code.Services.UserBalance;
using BlackJack.Code.Services.WebRequest;
using UnityEngine;
using Zenject;

namespace BlackJack.Code.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private SoundService _soundService;
        
        public override void InstallBindings()
        {
            RegisterSceneLoader();
            RegisterStaticDataProvider();
            RegisterCoroutineRunner();
            RegisterEntityContainer();
            RegisterSaveLoad();
            RegisterPersistentProgress();
            RegisterStaticData();
            RegisterUserBalance();
            RegisterSoundService();
            RegisterWebRequestService();
            RegisterCardMover();
            RegisterPersistentEntityFactory();
            RegisterGameFactory();
            RegisterUIFactory();
        }

        private void RegisterSceneLoader() =>
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        private void RegisterStaticDataProvider() =>
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle();

        private void RegisterCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void RegisterEntityContainer() =>
            Container.BindInterfacesTo<EntityContainer>().AsSingle();

        private void RegisterSaveLoad() =>
            Container.Bind<ISaveLoad>().To<PrefsSaveLoad>().AsSingle();

        private void RegisterPersistentProgress() =>
            Container.Bind<IPersistentProgress>().To<PersistentPlayerProgress>().AsSingle();        
        
        private void RegisterCardMover() =>
            Container.Bind<ICardMover>().To<CardMover>().AsSingle();

        private void RegisterStaticData() =>
            Container.Bind<IStaticData>().To<StaticData>().AsSingle();

        private void RegisterUserBalance() =>
            Container.Bind<IUserBalance>().To<UserBalance>().AsSingle();

        private void RegisterSoundService() =>
            Container.Bind<ISoundService>().FromInstance(_soundService).AsSingle();

        private void RegisterWebRequestService() =>
            Container.Bind<IWebRequestService>().To<WebRequestService>().AsSingle();

        private void RegisterPersistentEntityFactory() =>
            Container.Bind<IPersistentEntityFactory>().To<PersistentEntityFactory>().AsSingle();

        private void RegisterUIFactory() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

        private void RegisterGameFactory() =>
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
}