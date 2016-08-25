using UnityEngine;
using System.Collections;
using Eventro.Testapp.Enums;

namespace Eventro.Testapp.Common
{
	public class Constants
	{
		public static MixedRealityMode CURRENT_MIXED_REALITY_MODE = MixedRealityMode.AR_STEREO;
		public static GameMode CURRENT_GAME_MODE = GameMode.CubeTest;	

		#region Video Name
		public const string VIDEO_NAME = "ed_1024_512kb.mp4";
		public const string VIDEO_NAME2 = "EasyMovieTexture.mp4";

		#endregion

		#region Scene Names
		public const string  SCENE_NAME_1 = "Scene1";
		public const string  SCENE_NAME_2 = "Scene2";
		#endregion
	}

}