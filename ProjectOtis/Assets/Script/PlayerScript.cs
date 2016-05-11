using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[SerializeField]
	float Speed =.1f;
	[SerializeField]
	float MaxSpeed=.2f;
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
			if (Input.GetKey (KeyCode.Space)) 
			{
				Vector3 Temp = this.transform.position;
				Temp.y = Temp.y + MaxSpeed;
				this.transform.position = Temp;
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.y = Temp.y + Speed;
			this.transform.position = Temp;
			//Transform W = this.transform;
			//W.position
			//this.gameObject.transform.Translate(Vector3(PlayerTrans.position.x,PlayerTrans.position.y+Speed,PlayerTrans.position.x));
			}
		}
		if(Input.GetKey(KeyCode.A))
		{
			if(Input.GetKey(KeyCode.Space))
			{
				Vector3 Temp = this.transform.position;
				Temp.x = Temp.x - MaxSpeed;
				this.transform.position = Temp;
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.x = Temp.x - Speed;
			this.transform.position = Temp;
			}
		}
		if (Input.GetKey (KeyCode.S))
		{
			if(Input.GetKey(KeyCode.Space))
			{
				Vector3 Temp = this.transform.position;
				Temp.y = Temp.y - MaxSpeed;
				this.transform.position = Temp;
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.y = Temp.y - Speed;
			this.transform.position = Temp;
			}
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			if(Input.GetKey(KeyCode.Space))
			{
				Vector3 Temp = this.transform.position;
				Temp.x = Temp.x + MaxSpeed;
				this.transform.position = Temp;
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.x = Temp.x + Speed;
			this.transform.position = Temp;
			}
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
		MaxSpeed = .09f;
	}
	public void NormSpeedPlayer()
	{
		Speed = .1f;
		MaxSpeed = .2f;
	}
	public void WaterSlowPlayer()
	{
		Speed = .05f;
		MaxSpeed = .065f;
	}
}
