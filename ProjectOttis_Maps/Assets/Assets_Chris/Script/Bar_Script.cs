using UnityEngine;
using System.Collections;

public class Bar_Script : MonoBehaviour {
	[SerializeField]
	Vector3 Shower;
	[SerializeField]
	float posx;
	[SerializeField]
	float BadSpot;
	private bool test;
	private float Max=1f;
	float Fill;
	public bool Is_Nothing_In
	{
		get 
		{
			Shower = this.transform.localPosition;
			if (test)
				return false;
			if (Fill < 0f)
				Fill = 0f;
			//if (value <= .9f)
			//	value = 1f;
			//shower = Fill;
			if (Fill == 0f)
				return true;
			else
				return false;
		}
	}
	public bool Is_It_Full
	{
		get
		{
			Shower = this.transform.localPosition;
			if (test)
				return true;
			//shower = this.Fill;
			if (Fill == Max)
				return true;
			else
				return false;




		}
	}
	public float Value
	{
		get
		{
			return Fill;
		}
		set
		{
			//show = Fill;
			if (value >= 0&&value <1.1f) 
			{
				float tempm = 8.95f;
   				Vector3 gift = transform.localPosition;
				if (value < this.Fill) 
				{
					if (transform.localPosition.x == 57.60001f) {
						transform.localPosition = new Vector3 (41.1f, transform.localPosition.y, transform.localPosition.z);
					} else {
						transform.localPosition = new Vector3 (transform.localPosition.x - tempm, transform.localPosition.y, transform.localPosition.z);
					}
				}
				//if(transform.localPosition.x ==57.60001f)
				else 
				{
					if (transform.localPosition.x == 66.55001f) {
						//67.4
						transform.localPosition = new Vector3 (67.4f, transform.localPosition.y, transform.localPosition.z);
					} else {
						transform.localPosition = new Vector3 (transform.localPosition.x + tempm, transform.localPosition.y, transform.localPosition.z);
					}
				}
				float temp = value * 10;
				temp = Mathf.FloorToInt (temp);
				temp *= .1f;
				this.Fill = temp;
				//transform.position =new Vector3 (transform.position.x-(value*1.5f), transform.position.y, transform.position.z);
				transform.localScale = new Vector3 (value, transform.localScale.y, transform.localScale.z);
				if (value > 0.50) 
				{
					Color C = new Color (1 - (this.Fill - 0.50f) * 2, 1, 0);
					GetComponent<CanvasRenderer> ().SetColor (C);
				} else 
				{
					Color C = new Color (1, 0 + (this.Fill) * 2, 0);
					GetComponent<CanvasRenderer> ().SetColor (C);
				}
				Shower = this.transform.localPosition;
				//if (transform.localPosition.x == BadSpot)
				{
					//transform.localPosition = new Vector3 (posx, transform.localPosition.y, transform.localPosition.z);
				}
			}

		}
	}

	void Start()
	{
		Fill = Max;
		//shower = value;
		Color C = new Color(0, Max, 0);
		GetComponent<CanvasRenderer>().SetColor(C);
		//test = true;
		//transform.localScale = new Vector3 (.4f, transform.localScale.y, transform.localScale.z);
	}
}
