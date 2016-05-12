using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class DoorScript : MonoBehaviour {

	[SerializeField]
	GameObject KeyID;
	[SerializeField]
	Sprite Open;
	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			GameObject ky = play.GetPlayerKey ();
			if (ky == KeyID) 
			{
				SpriteRenderer spt = this.gameObject.GetComponent<SpriteRenderer> ();
				spt.sprite = Open;
                SceneManager.LoadScene("WinScene");
			}
		}
	}
}
