using UnityEngine;
using System.Collections;

namespace Eventro.Testapp.Controllers
{
	public class VideoPlaybackController
	{
		private PlayVideo vuforiaPlayVideo;

		public VideoPlaybackController ()
		{
			vuforiaPlayVideo = GameObject.Find ("ARCamera").GetComponent <PlayVideo> ();
		}

		#region Video Controller

		/// <summary>
		/// Play the video which the pointer has focused.
		/// </summary>
		internal void PlayVideoOf (string focusedObject)
		{
			VideoPlaybackBehaviour vpb;
			vpb = GameObject.Find (focusedObject).GetComponentInChildren<VideoPlaybackBehaviour> ();
			vuforiaPlayVideo.PlayFocusedVideo (vpb);
		}

		// Pause method is not wroking
		internal void PauseCurrentVideo ()
		{
			vuforiaPlayVideo.PauseCurrentPlayingVideo ();
		}

		#endregion
	}
}