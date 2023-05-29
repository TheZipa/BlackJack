using UnityEngine;

namespace BlackJack.Code.Data.StaticData.Configs
{
    [CreateAssetMenu(fileName = "Card Config", menuName = "Static Data/Card Config")]
    public class BlackJackCardConfig : ScriptableObject
    {
        public CardData[] CardData;
        public Sprite Shirt;
        public Vector2 SplitCardScale;
        [Range(0,1)]
        public float SplitCardAlpha;
    }
}