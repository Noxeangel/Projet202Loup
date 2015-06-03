using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingLogoBehaviour : MonoBehaviour {

    public float rotationSpeed = 100.0f;
    public float LoadingTime = 3.0f;
    public float _timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0.0f, 0.0f, -1.0f), rotationSpeed * Time.deltaTime);

        _timer += Time.deltaTime;
        if (_timer > LoadingTime)
        {
            GameObject.Find("_Manager").GetComponent<GameManager>().BeginStartUp();

            transform.parent.gameObject.SetActive(false);
        }
	}
}
