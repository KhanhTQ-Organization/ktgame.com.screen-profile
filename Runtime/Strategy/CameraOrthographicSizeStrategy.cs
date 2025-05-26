using UnityEngine;

namespace com.ktgame.screen_profile
{
    public class CameraOrthographicSizeStrategy : ScreenProfileStrategy<CameraProfile>
    {
        [SerializeField] private Camera _camera;

        protected override void OnApplyProfile(CameraProfile profile)
        {
            _camera.orthographicSize = profile.OrthographicSize;
        }
    }
}
