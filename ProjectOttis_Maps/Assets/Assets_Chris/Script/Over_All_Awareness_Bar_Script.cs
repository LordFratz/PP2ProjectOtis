using UnityEngine;
using System.Collections;

public class Over_All_Awareness_Bar_Script : MonoBehaviour {

	float value = 1f;
	public bool Is_Nothing_In
	{
		get 
		{
            if (value >= 99.99905f)
                value = 100f;
        
			if (value == 100f)
				return true;
			else
				return false;
		}
	}
	public float Value
	{
		get
		{
			return value;
		}
		set
		{
            if (value > 0 && value < 100f)
            {
                this.value = value;
                transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
                if (value > 0.50)
                {
                   // Color C = new Color(1 - (this.value - 0.50f) * 2, 1, 0);
                   // GetComponent<CanvasRenderer>().SetColor(C);
                }
                else 
				{
                    //Color C = new Color(1, 0 + (this.value) * 2, 0);
                    //GetComponent<CanvasRenderer>().SetColor(C);
                }
            }
		}
	}
	void Start()
	{
		value = 0;
		Color C = new Color(1, 0, 0);
		GetComponent<CanvasRenderer>().SetColor(C);
	}
}
