using System;
using Sirenix.OdinInspector;

namespace com.ktgame.screen_profile
{
    [Serializable]
    public class ScreenProfile
    {
        [ReadOnly] public string Name;
        [ReadOnly] public float AspectRatio;
    }
}
