using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ktgame.screen_profile
{
    [CreateAssetMenu(fileName = "ScreenProfile", menuName = "Ktgame/Screen Size")]
    public class ScreenSize : ScriptableObject
    {
        [SerializeField] private Vector2[] _screens = new Vector2[]
        {
            new Vector2(16, 9), new Vector2(16, 10), new Vector2(18, 9),
            new Vector2(39, 18), new Vector2(20, 9), new Vector2(21, 9),
            new Vector2(4, 3), new Vector2(3, 2)
        };

        public Vector2[] Screens => _screens;
    }
}
