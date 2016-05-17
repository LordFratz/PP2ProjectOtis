using UnityEngine;
using System.Collections;

public class Sticks : MonoBehaviour {

    public AudioClip stick;
	[SerializeField]
	GameObject SoundObj;
    // Use this for initialization
	void Start()
	{
		//SoundObj.GetComponent<Sound_Script> ().Dead ();
	}
    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySingle2(stick);
			GameObject Sound = Instantiate(SoundObj,_other.gameObject.transform.position,Quaternion.Euler(0,0,0))as GameObject;
        }
    }
}
