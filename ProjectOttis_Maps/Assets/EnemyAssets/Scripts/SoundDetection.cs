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
		//this.transform.position = EnemySpot.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		this.gameObject.transform.position = new Vector3 (EnemyID.transform.position.x, EnemyID.transform.position.y, EnemyID.transform.position.z);
	}

    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "sound")
        {
			EnemyID.GetComponent<BaseEnemy> ().AddAlertBySound (1f, this.gameObject.transform.position);
        }
    }
}
