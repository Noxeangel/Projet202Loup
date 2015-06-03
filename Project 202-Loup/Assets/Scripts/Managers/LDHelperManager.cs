#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

//[ExecuteInEditMode]
public class LDHelperManager : MonoBehaviour {
    [Range(0.0f, 100.0f)]
    public float percentProgress = 0.0f;

    public bool isLDHelperUsed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(GameObject.Find("_Manager").GetComponent<SpeedManager>().IntegrateSpeedCurve(PlayerIndex.PLAYER1));
	}
}
#endif
