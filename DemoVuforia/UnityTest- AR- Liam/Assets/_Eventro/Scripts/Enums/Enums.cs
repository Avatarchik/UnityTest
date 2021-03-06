﻿using UnityEngine;
using System.Collections;

namespace Eventro.Testapp.Enums
{
	public enum GameMode
	{
		// For VR Stereo
		CubeTest,
		// For AR Mono
		VideoPlayerTest,
	}

	public enum ButtonsType
	{
		// For VR Stereo
		Custom,
		Hotspot,
		VideoButton,

	}

	public enum MixedRealityMode{
		AR_STEREO,
		AR_MONO,
	}
}