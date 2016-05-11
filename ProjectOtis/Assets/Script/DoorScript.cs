using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	[SerializeField]
	GameObject KeyID;
	[SerializeField]
	Sprite Open;
	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "PlayerTag") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			GameObject ky = play.GetPlayerKey ();
			if (ky == KeyID) 
			{
				SpriteRenderer spt = this.gameObject.GetComponent<SpriteRenderer> ();
				spt.sprite = Open;
			}
		}
	}
}
