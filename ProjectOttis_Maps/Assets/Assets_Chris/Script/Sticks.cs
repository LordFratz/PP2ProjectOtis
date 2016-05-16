using UnityEngine;
using System.Collections;

public class Sticks : MonoBehaviour {

    public AudioClip stick;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySingle2(stick);
        }
    }
}
