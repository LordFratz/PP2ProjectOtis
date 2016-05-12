using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{

    [SerializeField]
    bool gethit;

    [SerializeField]
    float hp;

    [SerializeField]
    GameObject enemies;

    [SerializeField]
    float timer;

    private float mantimer;

    private float TimerMAX = 5f;

    // Use this for initialization
    void Start()
    {
        gethit = false;
        hp = 100.0f;
        timer = TimerMAX;
        mantimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
        }

        Gethurt();
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "enemy")
        {
            gethit = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "enemy")
        {
            gethit = false;
            timer = TimerMAX;
        }
    }

    void Hitcountdown(bool get)
    {
        if (get == true)
        {
            timer -= Time.deltaTime;
            mantimer -= Time.deltaTime;
        }
    }

    void Gethurt()
    {
        BaseEnemy enemy = enemies.GetComponent<BaseEnemy>();

        int type = enemy.GetType();
        switch (type)
        {
            case 0:
                
                if (enemy.CheckValidDistance())
                {
                    Hitcountdown(gethit);
                    if (mantimer <= 0)
                    {
                        hp -= enemy.damage;
                        mantimer = 3.0f;
                    }
                }
                break;
            case 1:
                Hitcountdown(gethit);
                if (timer <= 0)
                {
                    enemy.Pounce();
                    hp -= enemy.damage;
                    timer = TimerMAX;
                }
                break;
            default:
                break;
        }


    }
}