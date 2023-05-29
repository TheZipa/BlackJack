using System;
using UnityEngine;

namespace BlackJack.Code.Data.StaticData.Location
{
    [Serializable]
    public class Location
    {
        public Vector2 Position;
        public Quaternion Rotation;

        public Location(Vector2 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}