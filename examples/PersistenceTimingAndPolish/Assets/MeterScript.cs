using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour
{
	public string label;
	public Color meterColor;
	public Image meterFG;
	public Image meterFGBG;
	public Text labelText;

	float meterFeedbackRate = 0.4f;

	// Start is called before the first frame update
	void Start()
	{
		labelText.text = label;
		meterFG.color = new Color(meterColor.r, meterColor.g, meterColor.b, 1);
	}

	// Update is called once per frame
	void Update()
	{
		if (meterFGBG.fillAmount > meterFG.fillAmount) {
			meterFGBG.fillAmount -= meterFeedbackRate * Time.deltaTime;
		}
	}

	public void SetMeter(float val)
	{
		meterFG.fillAmount = val;
	}
}
