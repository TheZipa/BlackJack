using UnityEngine;

namespace BlackJack.Code.Data.StaticData.Configs
{
    [CreateAssetMenu(fileName = "BlackJack Settings Config", menuName = "Static Data/BlackJack Settings Config")]
    public class BlackJackSettingsConfig : ScriptableObject
    {
        public int StartBalance;
        public float AnimationSpeed;
        public string FirebaseKey;
        [Space(10)] public BetChipData[] BetChipData;
    }
}