using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
	[SerializeField]
	int IDToKey;
    public AudioClip key;
    void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
            SoundManager.instance.PlaySingle2(key);
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
