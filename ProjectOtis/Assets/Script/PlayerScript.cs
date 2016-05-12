using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[SerializeField]
	float Speed =.1f;
	[SerializeField]
	float MaxSpeed=.2f;
	private float Stamina = 1;
	private float HP = 1;
	[SerializeField]
	GameObject BarHP;
	[SerializeField]
	GameObject BarS;

	private int Timer = 50;
	private int StartTimerOnRecover;
	private int StartTimer;
	//private Transform PlayerTrans;

	[SerializeField]
	GameObject KeyID;
	// Use this for initialization
	void Start () {
		//PlayerTrans = this.gameObject.transform;
		KeyID = null;
		StartTimer = Timer;
		StartTimerOnRecover = Timer;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		bool PlayerRan = false;
		if(Input.GetKey(KeyCode.W))
		{
			if (Input.GetKey (KeyCode.Space)&& BarS.GetComponent<Bar_Script>().Is_Nothing_In == false) 
			{
				PlayerRan = true;
				Vector3 Temp = this.transform.position;
				Temp.y = Temp.y + MaxSpeed;
				this.transform.position = Temp;
				if (StartTimer > Timer)
					StartTimer = Timer;
				if (StartTimer == Timer)
				{
					Lower_Stamina ();
					StartTimer = 0;
				} else
					StartTimer++;
				//Lower_Stamina ();
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.y = Temp.y + Speed;
			this.transform.position = Temp;
				StartTimer++;
				//if (StartTimerOnRecover == Timer)
				//	ReGain_Stamina ();
				//else
				//	StartTimerOnRecover++;
			//Transform W = this.transform;
			//W.position
			//this.gameObject.transform.Translate(Vector3(PlayerTrans.position.x,PlayerTrans.position.y+Speed,PlayerTrans.position.x));
			}
		}
		if(Input.GetKey(KeyCode.A))
		{
			if(Input.GetKey(KeyCode.Space)&& BarS.GetComponent<Bar_Script>().Is_Nothing_In == false)
			{
				PlayerRan = true;
				Vector3 Temp = this.transform.position;
				Temp.x = Temp.x - MaxSpeed;
				this.transform.position = Temp;
				if (StartTimer > Timer)
					StartTimer = Timer;
				if (StartTimer == Timer)
				{
					Lower_Stamina ();
					StartTimer = 0;
				} else
					StartTimer++;
				//Lower_Stamina ();
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.x = Temp.x - Speed;
			this.transform.position = Temp;
				StartTimer++;
				//if (StartTimerOnRecover >= Timer)
				//	ReGain_Stamina ();
				//else
				//	StartTimerOnRecover++;
			}
		}
		if (Input.GetKey (KeyCode.S))
		{
			if(Input.GetKey(KeyCode.Space)&& BarS.GetComponent<Bar_Script>().Is_Nothing_In == false)
			{
				PlayerRan = true;
				Vector3 Temp = this.transform.position;
				Temp.y = Temp.y - MaxSpeed;
				this.transform.position = Temp;
				if (StartTimer > Timer)
					StartTimer = Timer;
				if (StartTimer == Timer)
				{
					Lower_Stamina ();
					StartTimer = 0;
				} else
					StartTimer++;
				//Lower_Stamina ();
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.y = Temp.y - Speed;
			this.transform.position = Temp;

				StartTimer++;
				//if (StartTimerOnRecover >= Timer)
				//	ReGain_Stamina ();
				//else
				//	StartTimerOnRecover++;
			}
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			PlayerRan = true;
			bool TempHolder=BarS.GetComponent<Bar_Script> ().Is_Nothing_In;
			if(Input.GetKey(KeyCode.Space)&& TempHolder == false)
			{
				PlayerRan = true;
				Vector3 Temp = this.transform.position;
				Temp.x = Temp.x + MaxSpeed;
				this.transform.position = Temp;
				if (StartTimer > Timer)
					StartTimer = Timer;
				if (StartTimer == Timer)
				{
					Lower_Stamina ();
					StartTimer = 0;
				} else
					StartTimer++;
				//Lower_Stamina ();
			}
			else
			{
			Vector3 Temp = this.transform.position;
			Temp.x = Temp.x + Speed;
			this.transform.position = Temp;
				//if (StartTimerOnRecover >= Timer)
				//	ReGain_Stamina ();
				//else
				//	StartTimerOnRecover++;
				StartTimer++;
			}
		}
		if (StartTimerOnRecover > Timer)
			StartTimerOnRecover = Timer;
		if (StartTimer > Timer)
			StartTimer = Timer;
		if (PlayerRan == false) 
		{
			if (StartTimerOnRecover == Timer)
				ReGain_Stamina ();
			else
				StartTimerOnRecover++;
		}
	}

	private void Lower_Stamina()
	{
		BarS.GetComponent<Bar_Script> ().Value -= .1f;
	}

	private void ReGain_Stamina()
	{
		BarS.GetComponent<Bar_Script> ().Value += .1f;
		StartTimerOnRecover = 0;
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
