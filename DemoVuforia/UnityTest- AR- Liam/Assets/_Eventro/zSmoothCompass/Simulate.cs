using UnityEngine;
using System.Collections;

public class Simulate : MonoBehaviour
{
	private Simulations simulationType = Simulations.Continuous;

	float currentValue = 0;
	float countLower = 0, countUpper = 360;

	//Increase/ increment spees
	private float calucateSpeed = 0.05f;


	// IT is the range of the variation when value will get Simulate
	//------------
	private float fromVariation = 30;
	private float toVariation = 40;

	public Vector2 VariationRange {
		get { 
			return new Vector2 (fromVariation, toVariation);
		}
	}
	//--------------------

	//It will let you very small amount of values to in opposite direction
	// as it was going
	private bool needFluctuation = true;
	private float fluctuationVlaue = 0.0001f;

	private float FluctuateValue {
		get {
			if (needFluctuation)
				return fluctuationVlaue;
			else
				return 0f;
		}
	}


	#region Init

	// Use this for initialization
	void Start ()
	{
		CheckValidations ();	
	}

	void CheckValidations ()
	{
		if (toVariation < fromVariation) {
			Debug.LogError ("Your Minimum variation range is greater than the Maximum!!");
			return;
		}
		if (countUpper < countLower) {
			Debug.LogError ("Your Minimum count range is greater than the Maximum!!");
			return;
		}
		if (calucateSpeed == 0) {
			Debug.LogError ("You increament speed is zero the code can't be execute");
			return;
		}

		if (simulationType == Simulations.ContinuousWithFluctuation)
			needFluctuation = true;

	}

	#endregion

	// Update is called once per frame
	void Update ()
	{
		switch (simulationType) {
		case Simulations.Continuous:
			ContineousSimulation ();
			break;
		}
	}

	private bool isIncrementing = true;

	private void ContineousSimulation ()
	{
		// 1. start the value from the lowe count and increase that
		// 2. when the vlaue will reach the 'fromVariation' then it will get loop between 'fromVariation' & 'toVariation'
		// 3. If fluctuation is 'True' then the fluctuateValue will add and decrease randomly 
		if (currentValue >= VariationRange.y) { // Now decrease the value
			isIncrementing = false;
		} else if (currentValue <= VariationRange.x) {// Now increase to remain in the range 
			isIncrementing = true;
		}
		CalculateVal (isIncrementing);
		print (currentValue);
	}

	private void CalculateVal (bool isInc, bool fluctuation = false)
	{
		if (isInc) {
			currentValue = currentValue + calucateSpeed;
		} else {
			currentValue = currentValue - calucateSpeed;
		}

	}
}

public enum Simulations
{
	Continuous,
	ContinuousWithFluctuation,
	WithLittleDelay,
}
