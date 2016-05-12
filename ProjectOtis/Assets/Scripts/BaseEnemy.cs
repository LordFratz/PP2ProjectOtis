using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BaseEnemy : MonoBehaviour {

    enum Enemy { RobotMan = 0, RobotDog = 1 };

    [SerializeField] private float heading;
    float timer;
    float wanderingtime;
    private float turntime;
    int random;
    [SerializeField] private bool followMode;
    [SerializeField] private bool BreadMode;
    [SerializeField] private bool AlertMode;
    [SerializeField] private bool InvestigateMode;
    Collider2D player;
    private Vector3 playerlastknown;
    [SerializeField] private float Basespeed;
    [SerializeField] private float AlertMax;
    private float speed;

    [SerializeField] float alertamount;

    Dictionary<int,Vector3> path = new Dictionary<int,Vector3>();
    Dictionary<int, Vector3> breadcrumbs = new Dictionary<int, Vector3>();
    int currentpoint;
    private int currentbreadcrumb;

    [SerializeField]
    Enemy type;

	// Use this for initialization
	void Start () {
        alertamount = 0.0f;
	    AlertMax = 30.0f;
        heading = 0.0f;
	    speed = Basespeed;
        currentpoint = 0;
	    currentbreadcrumb = 0;
        timer = 0;
        wanderingtime = 0;
        followMode = false;
        Vector3 temp = transform.position;
        temp.Set(4,4,0);
        path.Add(0, temp);
        temp.Set(2, 2, 0);
        path.Add(1, temp);
        breadcrumbs.Add(0, new Vector3(0,0));
	    
	    //path.Add(0, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (AlertMode)
        {
            alertamount += 0.1f;
        }
	    //if (alertamount > AlertMax / 2 && InvestigateMode)
	    //{
     //       Investigate();
	    //    return;
	    //}
        if (alertamount >= AlertMax)
        {
            switch (type)
            {
                case Enemy.RobotMan:
                    speed = Basespeed * 1.25f;
                    break;
                case Enemy.RobotDog:
                    speed = Basespeed * 2;
                    break;
                default:
                    break;
            }

        }
        if(followMode)
        {
            if(wanderingtime > 0)
            {
                Wandering();
                return;
            }
            Following();
            return;
        }
        else if (BreadMode)
        {
            Breading();
            return;
        }
        else
        {
            Patrolling();
        }
        BaseMove();
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
        Vector3 vectorToTarget = (player.transform.position - transform.position).normalized;
        heading = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(heading, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

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
            followMode = false;
            BreadMode = true;
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
                BreadMode = false;
                return;
            }
        }

        Vector3 vectorToTarget = (breadcrumbs[currentbreadcrumb] - transform.position).normalized;
        heading = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(heading, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);
        CorrectHeading();
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
                Vector3 vectorToTarget = (path[0] - transform.position).normalized;
                heading = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(heading, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
                CorrectHeading();
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

            Vector3 vectorToTarget = (path[currentpoint] - transform.position).normalized;
            heading = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(heading, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);
            CorrectHeading();

        }
    }

    void BaseMove()
    {
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
            followMode = true;
            InvestigateMode = false;
            AlertMode = true;
            
            wanderingtime = 0;
            player = obj;
        }
        //else if (obj.gameObject.tag == "wall")
        //{
        //    if (InvestigateMode)
        //    {
        //        InvestigateMode = false;
        //    }
        //}
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            AlertMode = false;
            wanderingtime = Random.Range(3,8);
            playerlastknown = player.transform.position;
        }
    }
}
