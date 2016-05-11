using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class BaseEnemy : MonoBehaviour {

    enum Enemy { RobotMan = 0, RobotDog = 1 };

    [SerializeField]
    float heading;
    float timer;
    float wanderingtime;
    int random;
    [SerializeField]
    bool followMode;
    Collider2D player;

    [SerializeField]
    float speed;

    float alertamount;

    Dictionary<int,Vector3> path = new Dictionary<int,Vector3>();
    int currentpoint;

    [SerializeField]
    Enemy type;

	// Use this for initialization
	void Start () {
        alertamount = 0.0f;
        heading = 0.0f;
        currentpoint = 0;
        timer = 0;
        wanderingtime = 0;
        followMode = false;
        Vector3 temp = transform.position;
        temp.Set(4,4,0);
        path.Add(0, temp);
        temp.Set(2, 2, 0);
        path.Add(1, temp);
        //path.Add(0, transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (alertamount >= 10)
        {
            switch (type)
            {
                case Enemy.RobotMan:
                    break;
                case Enemy.RobotDog:
                    speed *= 2;
                    break;
                default:
                    break;
            }

        }
        else if(followMode)
        {
            if(wanderingtime > 0)
            {
                Wandering();
                return;
            }
            Following();
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
    }

    void Wandering()
    {
        wanderingtime -= Time.deltaTime;
        if (wanderingtime <= 0)
        {
            followMode = false;
        }
        Standstill();
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = heading;
        transform.rotation = Quaternion.Euler(rotationVector);
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
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
            CorrectHeading();

        }
    }

    void BaseMove()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = heading;
        transform.rotation = Quaternion.Euler(rotationVector);
        transform.position = Vector3.MoveTowards(transform.position, path[currentpoint], speed * Time.deltaTime);
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

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            followMode = true;
            player = obj;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            wanderingtime = Random.Range(3,8);
        }
    }
}
