using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameState gameState = GameState.MENU;

    public float timeBeforeGameBegin = 2.0f;
    private float _timerBeforeBegin = 0.0f;

    public AudioManager _audioManager;

    public Transform character1Transform;
    public Transform character2Transform;

    public CameraBehaviour topCamera;
    public CameraBehaviour botCamera;

    public Transform countDownScreen;
    public Transform loadingScreen;

    public bool isCharacter1OnTop = true;

    void Awake ()
    {
        _audioManager = gameObject.GetComponent<AudioManager>();
    }

	// Use this for initialization
	void Start () {
	
	}
	


	// Update is called once per frame
	void Update () {

        if (gameState == GameState.GAME && GameObject.Find("Character1") == null && GameObject.Find("Character2") == null)
        {
            EndGameFail();
        }

        if(gameState == GameState.STARTUP)
        {
            if (countDownScreen == null)
            {
                GameObject.Find("CountDownScreen").SetActive(true);
                countDownScreen = GameObject.Find("CountDownScreen").transform;
                countDownScreen.gameObject.GetComponent<CountdownScreenBehaviour>().LaunchStartupIn(timeBeforeGameBegin);
            }


            _timerBeforeBegin += Time.deltaTime;

            

            if(_timerBeforeBegin >= timeBeforeGameBegin)
            {
                BeginGame();
                _timerBeforeBegin = 0.0f;

                
            }
        }

        if(gameState == GameState.GAME)
        {
#if UNITY_EDITOR
            /*
            if(Input.GetKeyUp(KeyCode.S))
            {
                SwapPlayers();
            }
             */
#endif
        }

	}

    void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case 0:
                gameState = GameState.MENU;
                break;
            case 1:
                gameState = GameState.MENU;
                break;
            case 2 :
                gameState = GameState.MENU;
                break;
            case 3:
                LoadLevel(0);
                break;
            case 4:
                gameState = GameState.ENDGAME_SUCCESS;
                break;
            case 5:
                gameState = GameState.ENDGAME_FAIL;
                break;
        }
    }

    public void LoadLevel(int level)
    {
        gameState = GameState.LOADING;
    }

    public void BeginStartUp()
    {

        gameState = GameState.STARTUP;

    }

    public void BeginGame()
    {
        gameState = GameState.GAME;
        //GameObject.Find("Character1").GetComponent<RaycastCharacterBehaviour>().hasRunBegan = true;
        //GameObject.Find("Character2").GetComponent<RaycastCharacterBehaviour>().hasRunBegan = true;

        if (character1Transform == null)
        {
            character1Transform = GameObject.Find("Character1").transform;
        }

        if (character2Transform == null)
        {
            character2Transform = GameObject.Find("Character2").transform;
        }

        topCamera = GameObject.Find("CameraPlayer1").GetComponent<CameraBehaviour>();
        botCamera = GameObject.Find("CameraPlayer2").GetComponent<CameraBehaviour>();

        isCharacter1OnTop = true;
    }

    public void EndGameSucess()
    {
        countDownScreen = null;
        Application.LoadLevel(4);
    }

    public void EndGameFail()
    {
        countDownScreen = null;
        Application.LoadLevel(5);
    }

    public void SwapPlayers()
    {
        Vector3 tmpPos = character2Transform.position;

        character2Transform.gameObject.transform.position = character1Transform.position;
        character1Transform.gameObject.transform.position = tmpPos;

        if(isCharacter1OnTop)
        {
            topCamera.target = character1Transform;
            botCamera.target = character2Transform;
        }
        else
        {
            botCamera.target = character1Transform;
            topCamera.target = character2Transform;
        }

        //GameObject.Find("Character1").GetComponent<RaycastCharacterBehaviour>().hasRunBegan = true;
        //GameObject.Find("Character2").GetComponent<RaycastCharacterBehaviour>().hasRunBegan = true;
    }
}

public enum GameState
{
    MENU,
    STARTUP,
    GAME,
    ENDGAME_SUCCESS,
    ENDGAME_FAIL,
    SHOP,
    LOADING,
}
