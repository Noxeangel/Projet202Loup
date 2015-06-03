using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CountdownScreenBehaviour : MonoBehaviour {

    private bool isOn = false;
    private float startupTime = 0.0f;
    private float _timer = 0.0f;
    private string displayString;
    public Text startupText;
    public Color beginColor;
	// Use this for initialization
	void Start () {
        beginColor = gameObject.GetComponent<Image>().color;
	}
	
	// Update is called once per frame
	void Update () {
	    if(isOn)
        {
            _timer += Time.deltaTime;
            displayString = "Race begins\nin:\n"+(startupTime - _timer).ToString("F2")+" seconds";
            startupText.text = displayString;

            if(_timer < startupTime)
            {
                gameObject.GetComponent<Image>().color = beginColor - new Color(0.0f, 0.0f, 0.0f, (_timer / startupTime));
            }
            else
            {
                isOn = false;
                gameObject.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, 1);
                gameObject.SetActive(false);
            }
        }
	}

    public void LaunchStartupIn(float _t)
    {
        if(!isOn)
        {
            isOn = true;
            startupTime = _t;
        }
    }
}
