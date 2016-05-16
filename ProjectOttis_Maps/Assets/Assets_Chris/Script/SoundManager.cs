using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource BackgroundMusic;
    public AudioSource SFX;
    public AudioSource SFX2;
    public static SoundManager instance = null;
	// Use this for initialization
	void Awake ()
    {
	    if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	public void PlaySingle(AudioClip clip)
    {
        SFX.clip = clip;
        SFX.Play();
    }
    public void PlaySingle2(AudioClip clip)
    {
        SFX2.clip = clip;
        SFX2.Play();
    }
}
