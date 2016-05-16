using UnityEngine;
using System.Collections;

public class WaterSpot_Script : MonoBehaviour {
    [SerializeField]
    Sprite one;
    [SerializeField]
    Sprite two;
    public AudioClip water;
    public AudioClip grass;
    bool first = true;
    int i = 0;
    void FixedUpdate()
    {
        if (i > 30)
        {
            SpriteRenderer spt = this.gameObject.GetComponent<SpriteRenderer>();
            if (first == false)
            {
                first = true;
                spt.sprite = one;
            }
            else
            {
                first = false;
                spt.sprite = two;
            }
            i = 0;
        }
        i++;
    }
    void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
            SoundManager.instance.PlaySingle2(water);
            SoundManager.instance.SFX.mute = true;
            PlayerScript play = _other.GetComponent<PlayerScript>();
			play.WaterSlowPlayer ();

		}
	}
	void OnTriggerExit2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Player") 
		{
            SoundManager.instance.SFX2.Stop();
            SoundManager.instance.SFX.mute = false;
            PlayerScript play = _other.GetComponent<PlayerScript>();
			play.NormSpeedPlayer ();
		}
	}
}
