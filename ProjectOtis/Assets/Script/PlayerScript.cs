using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[SerializeField]
	float Speed =.1f;
	//private Transform PlayerTrans;

	[SerializeField]
	GameObject KeyID;
	// Use this for initialization
	void Start () {
		//PlayerTrans = this.gameObject.transform;
		KeyID = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.W))
		{
			Vector3 Temp = this.transform.position;
			Temp.y = Temp.y + Speed;
			this.transform.position = Temp;
			//Transform W = this.transform;
			//W.position
			//this.gameObject.transform.Translate(Vector3(PlayerTrans.position.x,PlayerTrans.position.y+Speed,PlayerTrans.position.x));
		}
		if(Input.GetKey(KeyCode.A))
		{
			Vector3 Temp = this.transform.position;
			Temp.x = Temp.x - Speed;
			this.transform.position = Temp;
		}
		if (Input.GetKey (KeyCode.S))
		{
			Vector3 Temp = this.transform.position;
			Temp.y = Temp.y - Speed;
			this.transform.position = Temp;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			Vector3 Temp = this.transform.position;
			Temp.x = Temp.x + Speed;
			this.transform.position = Temp;
		}
	}

	public void SetKey(GameObject Obj)
	{
		if (Obj.tag == "Key") 
		{
			KeyID = Obj;
		}
	}

	public GameObject GetPlayerKey()
	{
		return KeyID;
	}

	public void SlowPlayer()
	{
		Speed = .065f;
	}
	public void NormSpeedPlayer()
	{
		Speed = .1f;
	}
}
