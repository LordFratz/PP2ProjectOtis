using UnityEngine;
using System.Collections;

public class Sound_Script : MonoBehaviour {

	private int UntilKillSelf;
	//private bool SoundMade;
	//private bool OneTime = true;
	// Use this for initialization
	void Start () 
	{
		UntilKillSelf = 50;
		//Vector3 here = Where.transform.position;
		//SoundMade = false;
		//TurnOff ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//if (UntilKillSelf == 0)
		//{
		//	TurnOff ();
		//}
		//else
		//{
		//	UntilKillSelf--;
		//}
	}
	void OnTriggerEnter2D(Collider2D _other)
	{
		//if (_other.gameObject.tag == "Player") 
		//{
			//TurnON ();
		//}
	}
	public int GetTimer()
	{
		return UntilKillSelf;
	}
	public void TurnOff()
	{
		//this.gameObject.SetActive (false);
		//SoundMade = false;
		//this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}
	public void TurnON()
	{
		//this.gameObject.SetActive (true);
		//SoundMade = true;
		//UntilKillSelf = 100;
		//this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}
	public void KILLSELF()
	{
		this.gameObject.SetActive (false);
		Destroy (this);
	}
}
