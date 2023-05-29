using System;
using BlackJack.Code.Data.Enums;
using UnityEngine;

namespace BlackJack.Code.Data.StaticData.Sounds
{
    [Serializable]
    public class AudioClipData
    {
        public AudioClip Clip;
        public SoundId Id;
    }
}