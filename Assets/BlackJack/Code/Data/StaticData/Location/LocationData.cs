using UnityEngine;

namespace BlackJack.Code.Data.StaticData.Location
{
    [CreateAssetMenu(fileName = "Location Data", menuName = "Static Data/Location Data")]
    public class LocationData : ScriptableObject
    {
        public float CardOffset;
        public Location DealerLocation;
        public Location PlayerLocation;
        public Location CardDeckLocation;
        public Location SplitLocation;
    }
}