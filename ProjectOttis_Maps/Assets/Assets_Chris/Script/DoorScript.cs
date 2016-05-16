using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class DoorScript : MonoBehaviour {

	[SerializeField]
	GameObject KeyID;
	[SerializeField]
	Sprite Open;
    public AudioClip door;
    void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
			PlayerScript play = _other.GetComponent<PlayerScript>();
			GameObject ky = play.GetPlayerKey ();
			if (ky == KeyID) 
			{
                SoundManager.instance.PlaySingle2(door);
                SpriteRenderer spt = this.gameObject.GetComponent<SpriteRenderer> ();
				spt.sprite = Open;
                SoundManager.instance.BackgroundMusic.Stop();
                SoundManager.instance.SFX.Stop();
                SceneManager.LoadScene("WinScene");
			}
		}
	}
}
