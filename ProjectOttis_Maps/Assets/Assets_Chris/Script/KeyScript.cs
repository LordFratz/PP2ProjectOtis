using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
	[SerializeField]
	int IDToKey;
	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			play.SetKey (this.gameObject);
			this.gameObject.SetActive (false);
		}
	}

	public int GetKeyID()
	{
		return IDToKey;
	}
}
