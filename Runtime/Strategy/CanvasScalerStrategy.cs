using UnityEngine;
using UnityEngine.UI;

namespace com.ktgame.screen_profile
{
    public class CanvasScalerStrategy : ScreenProfileStrategy<AspectProfile>
    {
        [SerializeField] private CanvasScaler _canvasScaler;

        protected override void OnApplyProfile(AspectProfile profile)
        {
            _canvasScaler.screenMatchMode = profile.ScreenMatchMode;
            _canvasScaler.matchWidthOrHeight = profile.Match;
        }
    }
}
