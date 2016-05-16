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
	private Color Yellow = new Color (1f, 1f, 0, 1f);
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
			bool Up = false;
			bool Down = false;
			if (value >= 0&&value <1.1f) 
			{
				//float tempm = 8.95f;
				float byHowMuch = 9.5f;
  				Vector3 gift = transform.localPosition;

				CanvasRenderer co =this.GetComponent<CanvasRenderer> ();
				Color BadColor = co.GetColor();
				if (value < this.Fill) 
				{
					Down = true;
					float tempAdd = value * 10;
					tempAdd = Mathf.FloorToInt (tempAdd);
					tempAdd *= .1f;
					float check = tempAdd - this.Fill;
					if (check == .1f) {
						transform.localPosition = new Vector3 ((Mathf.FloorToInt (transform.localPosition.x) + .4f) - byHowMuch, transform.localPosition.y, transform.localPosition.z);
					} else {
						transform.localPosition = new Vector3 ((Mathf.FloorToInt (transform.localPosition.x) + .4f) - Mathf.FloorToInt ((byHowMuch) * ((this.Fill - value) * 10)), transform.localPosition.y, transform.localPosition.y);
					}
					//if (transform.localPosition.x == 57.80001f) {
					//	transform.localPosition = new Vector3 (41.1f, transform.localPosition.y, transform.localPosition.z);
					//} else {
					//	transform.localPosition = new Vector3 (transform.localPosition.x - tempm, transform.localPosition.y, transform.localPosition.z);
					//}
				}
				//if(transform.localPosition.x ==57.60001f)
				else {
					Up = true;
					float tempDec = value * 10;
					tempDec = Mathf.FloorToInt (tempDec);
					tempDec *= .1f;
					float check = this.Fill - tempDec;
					if (check == .1f) {
						transform.localPosition = new Vector3 ((Mathf.FloorToInt (transform.localPosition.x) + .4f) - byHowMuch, transform.localPosition.y, transform.localPosition.z);
					} else {
						transform.localPosition = new Vector3 ((Mathf.FloorToInt (transform.localPosition.x) + .4f) - Mathf.FloorToInt ((byHowMuch) * ((this.Fill - value) * 10)), transform.localPosition.y, transform.localPosition.y);
					}
					//if (transform.localPosition.x == 57.80001f) 
					//{
					//	transform.localPosition = new Vector3 (58.2f, transform.localPosition.y, transform.localPosition.z);
					//} else 
					//{
					//	transform.localPosition = new Vector3 (transform.localPosition.x + tempm, transform.localPosition.y, transform.localPosition.z);
					//}
				}
				float temp = value * 10;
				temp = Mathf.FloorToInt (temp);
				temp *= .1f;
				this.Fill = temp;
				//transform.position =new Vector3 (transform.position.x-(value*1.5f), transform.position.y, transform.position.z);
				transform.localScale = new Vector3 (value, transform.localScale.y, transform.localScale.z);
				if (BadColor == Yellow) 
				{
					if (Up) 
					{
						//58.2f
						transform.localPosition = new Vector3 (58.2f, transform.localPosition.y, transform.localPosition.z);

					} else if (Down) 
					{
						//40.4f
						transform.localPosition = new Vector3 (40.4f, transform.localPosition.y, transform.localPosition.z);
					}
				}
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
			if (Fill == Max) 
			{
				transform.localPosition = new Vector3 (94.4f, transform.localPosition.y, transform.localPosition.z);
			}
		}
	}

	void Start()
	{
		Fill = Max;
		//Fill = .4f;
		//shower = value;
		Color C = new Color(0, Max, 0);
		GetComponent<CanvasRenderer>().SetColor(C);
		//test = true;
		//transform.localScale = new Vector3 (.4f, transform.localScale.y, transform.localScale.z);
	}
}
