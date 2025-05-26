using System;
using UnityEngine.UI;

namespace com.ktgame.screen_profile
{
    [Serializable]
    public class AspectProfile : ScreenProfile
    {
        public CanvasScaler.ScreenMatchMode ScreenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        public float Match = 0;
    }
}
