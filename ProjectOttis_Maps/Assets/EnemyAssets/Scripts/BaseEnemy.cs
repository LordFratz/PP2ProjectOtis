using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BaseEnemy : MonoBehaviour {

    enum Enemy { RobotMan = 0, RobotDog = 1 };

    enum Modes { BaseMode = 0, FollowMode = 1, WanderingMode = 2, BreadMode = 3, InvestigateMode = 4, AlertMode = 5 };

    [SerializeField]
    Enemy type;

    [SerializeField]
    GameObject playGuy;

    private PlayerScript playayaya;

    private Modes CurrentMode;

    private float mantimer;
    private float timerdog;
    private float timerhitdog;
    private float timerMax = 5f;

    //private bool gethit;

    [SerializeField] private float heading;
	Vector3 ranvec;

    //stats
	[SerializeField] float Basespeed;
    private float AlertMax;
    [SerializeField] private float alertamount;
    [SerializeField]
    GameObject alret_bar;

    private Vector3 playerlastknown;
    Collider2D player;
    float timer;
    float wanderingtime;
    private float robomantimer;
    int random;
    private float speed;
    public float damage;
    private bool decay;
    private bool PermaFollow;
    private bool pounce;
    private bool isseen;

    //pathing
    Dictionary<int,Vector3> path = new Dictionary<int,Vector3>();
    Dictionary<int, Vector3> breadcrumbs = new Dictionary<int, Vector3>();
    int currentpoint;
    private int currentbreadcrumb;

    //Path sets
    [SerializeField] private Vector3 coord1;
    [SerializeField] private Vector3 coord2;
    [SerializeField] private Vector3 coord3;
    [SerializeField] private Vector3 coord4;
    [SerializeField] private Vector3 coord5;
    [SerializeField] private Vector3 coord6;
    [SerializeField] private Vector3 coord7;
    [SerializeField] private Vector3 coord8;
    [SerializeField] private Vector3 coord9;
    [SerializeField] private Vector3 coord10;

    // Use this for initialization
    void Start ()
    {
        alertamount = 0.0f;
	    AlertMax = 10.0f;
        heading = 0.0f;
        robomantimer = 0;
        decay = true;
        PermaFollow = false;
        timerdog = timerMax;
        //gethit = false;
        mantimer = 0;
	    speed = Basespeed;
        switch (type)
        {
            case Enemy.RobotMan:
                damage = 20.0f;
                break;
            case Enemy.RobotDog:
                damage = 10.0f;
                break;
        }
        currentpoint = 0;
	    currentbreadcrumb = 0;
        timer = 0;
        wanderingtime = 0;
        SetPaths();
        breadcrumbs.Add(0, new Vector3(0, 0, 1));
        playayaya = playGuy.GetComponent<PlayerScript>();
        pounce = false;
        timerhitdog = 0f;
        isseen = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (decay)
	    {
	        alertamount -= 0.05f;
	        if (alertamount <= 0)
	        {
	            alertamount = 0;
	        }
	    }
	    if (alertamount >= AlertMax - 1 && !pounce)
	    {
            switch (type)
            {
                case Enemy.RobotMan:
                    speed = Basespeed * 1.5f;
                    break;
                case Enemy.RobotDog:
                    speed = Basespeed * 2.0f;
                    break;
            }
        }
	    switch (CurrentMode)
	    {
	        case Modes.BaseMode:
                Patrolling();
	            break;
            case Modes.FollowMode:
                Following();
	            break;
            case Modes.WanderingMode:
                Wandering();
	            break;
            case Modes.BreadMode:
                Breading();
	            break;
            case Modes.InvestigateMode:
                Investigate();
	            break;
            case Modes.AlertMode:
	            if(PermaFollow)
                    Following();
                else Wandering();
	            if (!pounce)
	            {
                    switch (type)
                    {
                        case Enemy.RobotMan:
                            speed = Basespeed*2.5f;
                            break;
                        case Enemy.RobotDog:
                            speed = Basespeed * 2.0f;
                            break;
                    }
			}
	            break;
	    }
        bool Change = alret_bar.GetComponent<Over_All_Awareness_Bar_Script>().Is_Nothing_In;
        if (Change == true)
        {

            CurrentMode = Modes.AlertMode;
        }
	    if (CurrentMode == Modes.FollowMode || PermaFollow == true)
	    {
            Hurt();
        }
	}

    void Standstill()
    {
        decay = true;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            random = Random.Range(0, 2);
            timer = Random.Range(0.5f, 1f);
        }
        if (random == 0)
        {
            heading -= 1f;
            CorrectHeading();
        }
        else
        {
            heading += 1f;
            CorrectHeading();
        }
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = heading;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    void Following()
    {
        decay = false;
        RotateTo(player.transform.position);

        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = heading;
        transform.rotation = Quaternion.Euler(rotationVector);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (CurrentMode != Modes.AlertMode)
        {
            currentbreadcrumb++;
            breadcrumbs.Add(currentbreadcrumb, transform.position);
            alertamount += 0.1f;
            if (alertamount >= AlertMax)
            {
                alertamount = AlertMax;
            }
        }
		PlayerSeen_Update_Bar();
    }

    void Wandering()
    {
        pounce = false;
        decay = true;
        if (CurrentMode != Modes.AlertMode)
        {
            wanderingtime -= Time.deltaTime;
            if (wanderingtime <= 0)
            {
                CurrentMode = Modes.BreadMode;
            }
            Standstill();
            currentbreadcrumb++;
            breadcrumbs.Add(currentbreadcrumb, transform.position);
        }
        else
        {
            wanderingtime -= Time.deltaTime;
            if (wanderingtime <= 0)
            {
                ranvec = new Vector3(Random.Range(-200, 100), Random.Range(-100, 20), 0);
                wanderingtime = 3.0f;
            }
			if(ranvec != null) 
			{
				RotateTo(ranvec);
				transform.position = Vector3.MoveTowards(transform.position, ranvec, speed * Time.deltaTime);
			}
        }
    }

    void Breading()
    {
        decay = true;
        while(transform.position == breadcrumbs[currentbreadcrumb])
        {
            breadcrumbs.Remove(currentbreadcrumb);
            currentbreadcrumb--;
            if (currentbreadcrumb <= 0)
            {
                CurrentMode = Modes.BaseMode;
                return;
            }
        }
        RotateTo(breadcrumbs[currentbreadcrumb]);
        transform.position = Vector3.MoveTowards(transform.position, breadcrumbs[currentbreadcrumb], speed * Time.deltaTime);
    }

    void Investigate()
    {
        if (transform.position != playerlastknown)
        {
            RotateTo(playerlastknown);
            transform.position = Vector3.MoveTowards(transform.position, playerlastknown, speed*Time.deltaTime);
            decay = false;
        }
        else
        {
            decay = true;
            CurrentMode = Modes.WanderingMode;
        }
    }

    void Patrolling()
    {
        if (path.Count <= 0)
        {
            Standstill();
        }
        else if (path.Count == 1)
        {
            if (transform.position == path[0])
            {
                Standstill();
            }
            else
            {
                RotateTo(path[0]);
            }
        }
        else
        {
            if (transform.position == path[currentpoint])
            {
                currentpoint++;
                if (currentpoint >= path.Count)
                {
                    currentpoint = 0;
                }
            }
            RotateTo(path[currentpoint]);
        }
        transform.position = Vector3.MoveTowards(transform.position, path[currentpoint], Time.deltaTime * speed);
    }

    void CorrectHeading()
    {
        if (heading < 0)
        {
            heading = 360f + heading;
        }
        else if (heading > 360)
        {
            heading = 360f - heading;
        }
    }

    void RotateTo(Vector3 pos)
    {
        Vector3 vectorToTarget = (pos - transform.position).normalized;
        heading = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(heading, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);
        CorrectHeading();
    }

    public void AddAlertBySound(float sound, Vector3 playerPos)
    {
        playerlastknown = playayaya.transform.position;
        alertamount += sound;
        CurrentMode = Modes.InvestigateMode;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            isseen = true;
			SpriteRenderer PlayVis = obj.gameObject.GetComponent<SpriteRenderer> ();
			SpriteRenderer CanSee = PlayVis;
			if(CanSee.color.a != .5f)
			{
            	if (CurrentMode != Modes.AlertMode)
            	{
            	    CurrentMode = Modes.FollowMode;
            	}
            	else
            	{
            	    PermaFollow = true;
            	}
            	wanderingtime = 0;
            	player = obj;
			}
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player" && isseen)
        {
            if (CurrentMode != Modes.AlertMode)
            {
                CurrentMode = Modes.WanderingMode;
                wanderingtime = Random.Range(3, 8);
            }
            else
            {
                PermaFollow = false;
                wanderingtime = 3.0f;
            }
            playerlastknown = player.transform.position;
        }
    }

    void SetPaths()
    {
        Vector3 temp = new Vector3(0,0,0);
        if (coord1 != temp)
        {
            path.Add(currentpoint, coord1);
            currentpoint++;
        }
        if (coord2 != temp)
        {
            path.Add(currentpoint, coord2);
            currentpoint++;
        }
        if (coord3 != temp)
        {
            path.Add(currentpoint, coord3);
            currentpoint++;
        }
        if (coord4 != temp)
        {
            path.Add(currentpoint, coord4);
            currentpoint++;
        }
        if (coord5 != temp)
        {
            path.Add(currentpoint, coord5);
            currentpoint++;
        }
        if (coord6 != temp)
        {
            path.Add(currentpoint, coord6);
            currentpoint++;
        }
        if (coord7 != temp)
        {
            path.Add(currentpoint, coord7);
            currentpoint++;
        }
        if (coord8 != temp)
        {
            path.Add(currentpoint, coord8);
            currentpoint++;
        }
        if (coord9 != temp)
        {
            path.Add(currentpoint, coord9);
            currentpoint++;
        }
        if (coord10 != temp)
        {
            path.Add(currentpoint, coord10);
            currentpoint++;
        }
        currentpoint = 0;
    }

    public new int GetType()
    {
        switch (type)
        {
            case Enemy.RobotMan:
                return 0;
            case Enemy.RobotDog:
                return 1;
        }
        return -1;
    }

    public bool CheckValidDistance()
    {
		if(player!= null)
		{
        	if (Mathf.Sqrt(Mathf.Pow(transform.position.x - playayaya.transform.position.x, 2) + Mathf.Pow(transform.position.y - playayaya.transform.position.y, 2)) <= transform.lossyScale.x * 1.4)
        	{
        	    return true;
        	}
		}
		return false;
    }

    void ManSpeed()
    {
        robomantimer -= Time.deltaTime;
        if (robomantimer <= 0)
        {
            robomantimer = 0.6f;
            speed += 1.0f;
            if (speed >= 5.0f)
            {
                speed = 0;
            }
        }
    }



    private void PlayerSeen_Update_Bar()
    {
        alret_bar.GetComponent<Over_All_Awareness_Bar_Script>().Value += .2f;

		bool Change = alret_bar.GetComponent<Over_All_Awareness_Bar_Script> ().Is_Nothing_In;
		if(Change == true)
		{
		   
			CurrentMode = Modes.AlertMode;
			PermaFollow = true;
		}
    }

    void Hitcountdown(bool get)
    {
        if (get)
        {
            timerdog -= Time.deltaTime;
            mantimer -= Time.deltaTime;
        }
    }

    void Hurt()
    {
        PlayerScript plyr = playGuy.GetComponent<PlayerScript>();
        switch (type)
        {
            case Enemy.RobotMan:
                Hitcountdown(plyr.gethit);
                if (CheckValidDistance())
                {
                    if (mantimer <= 0)
                    {
					plyr.GetComponent<PlayerScript>().GetDame(damage);
                        mantimer = 3.0f;
                    }
                }
                break;
            case Enemy.RobotDog:
                timerdog -= Time.deltaTime;
                timerhitdog -= Time.deltaTime;
                if (timerdog <= 0)
                {
                    speed *= 10;
                    pounce = true;
                    timerdog = timerMax;
                }
                if (CheckValidDistance() && timerhitdog <= 0)
                {
                    pounce = false;
                    speed = Basespeed * 2;
                    plyr.GetComponent<PlayerScript>().GetDame(damage);
                    timerhitdog = .5f;
                }
                break;
        }
    }

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
}
