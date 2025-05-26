using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ktgame.screen_profile
{
   	public abstract class ScreenProfileStrategy<T> : MonoBehaviour where T : ScreenProfile, new()
	{
		[SerializeField] private ScreenSize _screenSize;
		[SerializeField] private bool _isPortrait;
		[SerializeField, TableList] private List<T> _profiles;

		public bool IsPortrait => _isPortrait;

		public List<T> Profiles => _profiles;

		public float CurrentScreenAspect
		{
			get
			{
#if UNITY_EDITOR
				var mainGameViewSize = GetGameViewSize();
				return (_isPortrait ? mainGameViewSize.y : mainGameViewSize.x) /
					   (_isPortrait ? mainGameViewSize.x : mainGameViewSize.y);
#endif
				if (_isPortrait)
				{
					return (float)Screen.height / (float)Screen.width;
				}
				else
				{
					return (float)Screen.width / (float)Screen.height;
				}
			}
		}

		protected abstract void OnApplyProfile(T profile);

		private Vector2 _resolution;

		protected virtual void Awake()
		{
			_resolution = new Vector2(Screen.width, Screen.height);
		}

		protected virtual void Start()
		{
			if (_profiles != null && _profiles.Count > 0)
			{
				ApplyProfile();
			}
		}

		[Button]
		public void ApplyProfile()
		{
			var profile = FindClosestScreenSize(CurrentScreenAspect);
			Debug.Log("Profile: " + profile.Name);
			OnApplyProfile(profile);
		}

		private T FindClosestScreenSize(float aspect)
		{
			return _profiles.OrderBy(n => Math.Abs(n.AspectRatio - aspect)).First();
		}

#if UNITY_EDITOR
		private void LateUpdate()
		{
			if (Math.Abs(_resolution.x - Screen.width) > Mathf.Epsilon ||
				Math.Abs(_resolution.y - Screen.height) > Mathf.Epsilon)
			{
				_resolution = new Vector2(Screen.width, Screen.height);
				Debug.Log("Detect canvas change size. Auto change this: " + gameObject.name);
				ApplyProfile();
			}
		}

		private Vector2 GetGameViewSize()
		{
			var type = Type.GetType("UnityEditor.GameView,UnityEditor");
			var getSizeOfMainGameView = type?.GetMethod("GetSizeOfMainGameView",
				System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
			var resolution = getSizeOfMainGameView?.Invoke(null, null);
			return resolution == null ? Vector2.zero : (Vector2)resolution;
		}

		[Button]
		public void CreateEmptyProfile()
		{
			_profiles = new List<T>();
			foreach (var size in _screenSize.Screens)
			{
				var x = (int)size.x;
				var y = (int)size.y;

				var aspectRatio = (float)x / (float)y;

				var alreadyAdded = false;
				foreach (var profile in _profiles)
				{
					if (Math.Abs(profile.AspectRatio - aspectRatio) < Mathf.Epsilon)
					{
						alreadyAdded = true;
						break;
					}
				}

				if (!alreadyAdded)
				{
					var profile = new T { Name = $"{x}:{y}", AspectRatio = aspectRatio };
					_profiles.Add(profile);
				}
			}

			_profiles = _profiles.OrderBy(x => x.AspectRatio).ToList();
		}
#endif
	}
}
