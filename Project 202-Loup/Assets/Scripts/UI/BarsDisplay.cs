using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarsDisplay : MonoBehaviour {

	public float actualValue = 100;
	public float maxValue = 100;


	public Image bgBegin;
	public Image bgMiddle;
	public Image bgEnd;

	public Image barBegin;
	public Image barMiddle;
	public Image barEnd;


	public Text textDisplay;
	public bool isPercentText;

	// Use this for initialization
	void Start () {

        UpdateBar();


	}
	
	// Update is called once per frame
	void Update () {
		UpdateBar();
		UpdateText();
	}

	private void UpdateBar()
	{
        
        barMiddle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        barMiddle.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f , barBegin.GetComponent<RectTransform>().anchorMin.y);
        barMiddle.GetComponent<RectTransform>().anchorMax = new Vector2(0.1f + (actualValue / maxValue) * 0.8f, barBegin.GetComponent<RectTransform>().anchorMax.y);

        barEnd.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        barEnd.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f + (actualValue / maxValue) * 0.8f, barBegin.GetComponent<RectTransform>().anchorMin.y);
        barEnd.GetComponent<RectTransform>().anchorMax = new Vector2(0.1f + 0.05f + (actualValue / maxValue) * 0.8f, barBegin.GetComponent<RectTransform>().anchorMax.y);

	}

	private void UpdateText()
	{
		if(isPercentText)
		{
			textDisplay.text = (int)((actualValue/maxValue) * 100) +" % ";
		}
		else
		{
			textDisplay.text = (int)actualValue +" / "+(int)maxValue;
		}

	}
}
