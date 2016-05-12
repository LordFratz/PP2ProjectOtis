using UnityEngine;
using System.Collections;

public class SoundDetection : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyID;

    private BaseEnemy script;

	// Use this for initialization
	void Start () {
        script = EnemyID.GetComponent<BaseEnemy>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "sound")
        {
            //script.AddAlertBySound(1.0f, obj.transform.position);
        }
    }
}
