using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelMenuBehaviour : MonoBehaviour {
    private EventSystem m_eventSystem;

    // Use this for initialization
    void Start()
    {
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        m_eventSystem.SetSelectedGameObject(GameObject.Find("Level1"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBackClicked()
    {
        Application.LoadLevel(1);
    }

    public void OnLevelClicked(int _levelNumber)
    {
        Application.LoadLevel("TestScene");
    }
}
