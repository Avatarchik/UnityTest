using UnityEngine;
using System.Collections;

public class GyroRotate : MonoBehaviour
{
	[Range (1, 3)]
	public float senstivity = 1.2f;

	// Use this for initialization
	void Start ()
	{
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.Rotate (0, Input.gyro.rotationRateUnbiased.y, 0);
	}
}
