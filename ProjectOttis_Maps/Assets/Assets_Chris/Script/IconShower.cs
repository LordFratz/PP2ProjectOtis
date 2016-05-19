using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IconShower : MonoBehaviour {
	[SerializeField]
	Sprite Icon;
	// Use this for initialization
	void Start () {
		this.GetComponent<CanvasRenderer> ().SetAlpha (.1f);
	}

	public void PickedUp()
	{
		this.GetComponent<CanvasRenderer> ().SetAlpha (1f);
		Image spt = this.GetComponent<Image> ();// = Icon;
		spt.sprite = Icon;
		//this.GetComponent<Sprite> () = spt;

	}
}
