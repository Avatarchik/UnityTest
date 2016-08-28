using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool smoothMovement = false;
	public Smooth smoothType = Smooth.WithDelyInSeconds;
	private Simulate simulate;

	private float delayInSeconds = 0.1f;
	private float simulatedValue = 0;

	// Use this for initialization
	void Start ()
	{
		simulate = GameObject.Find ("Scripts").GetComponent<Simulate> ();

		if (smoothType == Smooth.WithDelyInSeconds)
			InvokeRepeating ("GetValuesWithDelayInterval", 0, delayInSeconds);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (smoothType) {
		case Smooth.Natural:
			SetEulerAngle (simulate.GetSimulatedValue ());
			break;
		case Smooth.WithDelyInSeconds:
			SetEulerAngle (delayReturn);
			break;
		}
	}

	private void SetEulerAngle (float value)
	{
		if (smoothMovement) {
			Quaternion target = Quaternion.Euler (
				                    gameObject.transform.eulerAngles.x,
				                    value,
				                    gameObject.transform.eulerAngles.z);

			transform.rotation = Quaternion.Slerp (transform.rotation,
				target, 1f * Time.deltaTime);
		} else {
			gameObject.transform.eulerAngles = new Vector3 (
				gameObject.transform.eulerAngles.x,
				value,
				gameObject.transform.eulerAngles.z
			);
		}
		
	}

	private float delayReturn = 0;

	private float GetValuesWithDelayInterval ()
	{
		delayReturn = simulate.GetSimulatedValue ();
		return delayReturn;
	}
}

public enum Smooth
{
	// It will send value to the player as as-it-is
	Natural,

	// It will send value to the player as with some delays
	WithDelyInSeconds,

	
}
