using UnityEngine;
using System.Collections;

public class WaterSpot_Script : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			play.WaterSlowPlayer ();

		}
	}
	void OnTriggerExit2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			play.NormSpeedPlayer ();
		}
	}
}
