using BlackJack.Code.Core.UI;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Core.UI.Menu;
using BlackJack.Code.Core.UI.TopPanel;
using UnityEngine;

namespace BlackJack.Code.Services.Factories.UIFactory
{
    public interface IUIFactory
    {
        GameObject CreateRootCanvas();
        MainMenuView CreateMainMenu(Transform parent);
        TopPanelView CreateTopPanel(Transform parent);
        UserInfoView CreateUserInfoView(Transform parent);
        Popup CreateWinPopUp(Transform parent);
        BetChipsPanel CreateBetChips(Transform parent);
        PlayerChoiceButtons CreatePlayerChoiceButtons(Transform parent);
        RegisterScreenView CreateRegisterScreenView(Transform parent);
    }
}