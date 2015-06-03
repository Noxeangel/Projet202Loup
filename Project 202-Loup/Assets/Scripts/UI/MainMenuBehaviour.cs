using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenuBehaviour : MonoBehaviour {

    private EventSystem m_eventSystem;

	// Use this for initialization
	void Start () {
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        m_eventSystem.SetSelectedGameObject(GameObject.Find("PlayButton"));
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnPlayButtonPressed()
    {
        Application.LoadLevel(2);
    }

    public void OnOptionButtonPressed()
    {

    }
}
