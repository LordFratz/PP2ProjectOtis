using UnityEngine;
using System.Collections;

public class BushScript : MonoBehaviour {

	private short NumUse=2;
	[SerializeField]
	Sprite LastLook;

	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "PlayerTag") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			SpriteRenderer co = _other.GetComponent<SpriteRenderer> ();
			Color lowview = co.color;
			lowview.a = .5f;
			co.color = lowview;
			play.SlowPlayer ();
			NumUse--;
		}
	}
	void OnTriggerExit2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "PlayerTag") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			SpriteRenderer co = _other.GetComponent<SpriteRenderer> ();
			Color lowview = co.color;
			lowview.a = 1f;
			co.color = lowview;
			play.NormSpeedPlayer ();
			if (NumUse == 1)
			{
				SpriteRenderer spt = this.gameObject.GetComponent<SpriteRenderer> ();
				spt.sprite = LastLook;
			}
			if (NumUse == 0)
			{
				this.gameObject.SetActive (false);
			}

		}
	}
}
