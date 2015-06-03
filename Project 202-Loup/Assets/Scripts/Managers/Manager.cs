using UnityEngine;
using System.Collections;


[RequireComponent(typeof (SaveManager))]
[RequireComponent(typeof(GameManager))]

public class Manager: MonoBehaviour {

    public bool isDebugManager = false;

    public static SaveManager saver;

    public static GameManager game;

    void Awake()
    {
    
        saver = gameObject.GetComponent<SaveManager>();

        game = gameObject.GetComponent<GameManager>();

    }
    // Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        
        if(isDebugManager && Application.loadedLevel == 3)
        {
            Destroy(this.gameObject);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
