using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BaseEnemy : MonoBehaviour {

    enum Enemy { RobotMan = 0, RobotDog = 1 };

    enum Modes { BaseMode = 0, FollowMode = 1, WanderingMode = 2, BreadMode = 3, InvestigateMode = 4, AlertMode = 5 };

    [SerializeField]
    Enemy type;

    private Modes CurrentMode;

    [SerializeField] private float heading;

    //stats
    private float Basespeed;
    private float AlertMax;
    [SerializeField] private float alertamount;


    private Vector3 playerlastknown;
    Collider2D player;
    float timer;
    float wanderingtime;
    int random;
    private float speed;

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
        speed = Basespeed = 0.5f;
        alertamount = 0.0f;
	    AlertMax = 30.0f;
        heading = 0.0f;
	    speed = Basespeed;
        currentpoint = 0;
	    currentbreadcrumb = 0;
        timer = 0;
        wanderingtime = 0;
        SetPaths();
        breadcrumbs.Add(0, new Vector3(0, 0, 1));
    }
	
	// Update is called once per frame
	void Update () {
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
                //To be implemented
	            break;
            case Modes.AlertMode:
	            switch (type)
	            {
	                case Enemy.RobotMan:
	                    speed = Basespeed * 1.25f;
	                    break;
                    case Enemy.RobotDog:
	                    speed = Basespeed * 2.0f;
	                    break;
	            }
	            break;
	    }
    }

    void Standstill()
    {
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
        RotateTo(player.transform.position);

        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = heading;
        transform.rotation = Quaternion.Euler(rotationVector);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        currentbreadcrumb++;
        breadcrumbs.Add(currentbreadcrumb, transform.position);
    }

    void Wandering()
    {
        wanderingtime -= Time.deltaTime;
        if (wanderingtime <= 0)
        {
            CurrentMode = Modes.BreadMode;
        }
        Standstill(); //replace with future implementation
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = heading;
        transform.rotation = Quaternion.Euler(rotationVector);
        currentbreadcrumb++;
        breadcrumbs.Add(currentbreadcrumb, transform.position);
    }

    void Breading()
    {
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
        transform.position = Vector3.MoveTowards(transform.position, breadcrumbs[currentbreadcrumb], Time.deltaTime * speed);
    }

    //void Investigate()
    //{
    //    Vector3 vectorToTarget = (playerlastknown - transform.position).normalized;
    //    heading = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
    //    Quaternion q = Quaternion.AngleAxis(heading, Vector3.forward);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);
    //    CorrectHeading();
    //}

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

    //public void AddAlertBySound(float sound, Vector3 playerPos)
    //{
    //    playerlastknown = playerPos;
    //    alertamount += sound;
    //    if (alertamount > AlertMax / 2)
    //    {
    //        InvestigateMode = true;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            CurrentMode = Modes.FollowMode;
            wanderingtime = 0;
            player = obj;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            CurrentMode = Modes.WanderingMode;
            wanderingtime = Random.Range(3,8);
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
}
