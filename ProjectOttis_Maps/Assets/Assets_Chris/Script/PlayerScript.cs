using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	[SerializeField]
	float Speed =.1f;
	[SerializeField]
	float MaxSpeed=.2f;
	private float Stamina = 1;
	private float HP = 100;
	public bool gethit;
	[SerializeField]
	GameObject BarHP;
	[SerializeField]
	GameObject BarS;
    [SerializeField]
    GameObject enemies;
    [SerializeField]
    float timer;
	[SerializeField]
	GameObject SoundObj;
    private float mantimer;
	private bool PlayerRan=false;
    private float TimerMax = 5f;

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
        gethit = false;
        timer = TimerMax;
        mantimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //gethurt();
	}

	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.W))
		{
			if (Input.GetKey (KeyCode.P)) 
			{
				//if(SoundObj != null)

			}
			if (Input.GetKey (KeyCode.Space)&& BarS.GetComponent<Bar_Script>().Is_Nothing_In == false) 
			{
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
			//bool TempHolder=BarS.GetComponent<Bar_Script> ().Is_Nothing_In;
			if(Input.GetKey(KeyCode.Space)&& BarS.GetComponent<Bar_Script>().Is_Nothing_In == false)
			{
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
		if (Input.GetKey (KeyCode.Space) == false)
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

	public void GetDame(float Dame)
	{
		HP -= Dame;
		if(HP <=0)
		{
			SceneManager.LoadScene ("LoseScene");
		}
		BarHP.GetComponent<Bar_Script> ().Value -= (Dame * .01f);
	}

	private void ReGain_Stamina()
	{
		if (BarS.GetComponent<Bar_Script> ().Is_It_Full == false) 
		{
			
			BarS.GetComponent<Bar_Script> ().Value += .1f;
		}
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

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "enemy")
        {
            gethit = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "enemy")
        {
            gethit = false;
            timer = TimerMax;
        }
    }

    //void Hitcountdown(bool get)
    //{
    //    if(get)
    //    {
    //        timer -= Time.deltaTime;
    //        mantimer -= Time.deltaTime;
    //    }
    //}

    //void gethurt()
    //{
    //    BaseEnemy enemy = enemies.GetComponent<BaseEnemy>();

    //    int type = enemy.GetType();
    //    switch(type)
    //    {
    //        case 0:
    //            if(enemy.CheckValidDistance())
    //            {
    //                Hitcountdown(gethit);
    //                if(mantimer <= 0)
    //                {
    //                    HP -= enemy.damage;
				//	if (HP <= 0) 
				//	{

				//	}
				//	BarHP.GetComponent<Bar_Script> ().Value -= (enemy.damage*.01f);
    //                    mantimer = 3.0f;
    //                }
    //            }
    //            break;
    //        case 1:
    //            Hitcountdown(gethit);
    //            if(timer <= 0)
    //            {
    //                enemy.Pounce();
    //                HP -= enemy.damage;
				//BarHP.GetComponent<Bar_Script> ().Value -= (enemy.damage*.01f);
    //                timer = TimerMax;
    //            }
    //            break;
    //    }
    //}
}
