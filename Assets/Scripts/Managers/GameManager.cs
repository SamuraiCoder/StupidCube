using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;
using Util;

public class GameManager : MonoBehaviour {

    public GameObject StupidCube;
    public float bridgeGap;
    public GameObject movingBridgePrefab;
    public GameObject rotatingBridgePrefab;
    public GameObject enemy;

    private GameState m_GameState;
    public GameState GameState
    {
        get { return m_GameState; }
        set { m_GameState = value; }
    }

    #region SINGLETON
    private static GameManager _instance = null;

    public static GameManager SINGLETON
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("[GameManager] Service did not start!!");
            }
            return _instance;
        }
    }
 #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void Start()
    {
        if (_instance != null)
        {
            Debug.Log("[GameManager] GameManager started");
        }
        else
        {
            Debug.LogError("[GameManager] GameManager failed!");
        }

        m_GameState = GameState.START;        
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop ()
    {
        switch(m_GameState)
        {
            case GameState.START:
            {
                yield return StartCoroutine(OnStartActions ());
                break;
            }
            case GameState.GAME:
            {
                yield return StartCoroutine(CheckBuildAnotherBridge ());
                break;
            }
            case GameState.PAUSE:
            {
                break;
            }
            case GameState.END:
            {
                break;
            }
        }
        //We create to keep this running as long as is not the end of the game.
       if(m_GameState != GameState.END)
       {
            StartCoroutine (GameLoop ());
       }
    }

#region OnStartActions
    private IEnumerator OnStartActions()
    {
        //First bridge moving
        yield return UtilityManager.SINGLETON.WaitAndExecute(2, CreateFirstBridge);
        //Second bridge rotating
        yield return UtilityManager.SINGLETON.WaitAndExecute(2, CreateSecondBridge);
        //We make the enemy move
        yield return UtilityManager.SINGLETON.WaitAndExecute(3, EnemyStarts);

        //After init, we change the status of the game to make it starts
        m_GameState = GameState.GAME;
    }
    void CreateFirstBridge()
    {
       GameObject mBridge = Instantiate(movingBridgePrefab);
       mBridge.GetComponent<MovingBridge>().speedClosing = 0.4f;
    }
    void CreateSecondBridge()
    {
       GameObject rBridge = Instantiate(rotatingBridgePrefab);
       rBridge.GetComponent<RotatingBridge>().speedClosing = 0.3f;
    }
    void EnemyStarts()
    {
        enemy.GetComponent<WaterMovement>().shouldMove = true;
    }
#endregion

#region CheckBuildAnotherBridge
    private IEnumerator CheckBuildAnotherBridge()
    {
        //Avoid to create more bridges if StupidCube is not grounded in one...
        if(StupidCube.GetComponent<StupidCubeController>().isGrounded)
        {
            //Then we compare if the cube is the highest element on the bridge chain
            Vector3 highestPos = UtilityManager.SINGLETON.SearchHighestGameObjectY("bridges");
            Vector3 stupidCubePos = StupidCube.transform.position;
            //Debug.Log("Calculated HighestPos: " + highestPos.y + "| StupidCube: " + stupidCubePos.y);

            if(stupidCubePos.y > highestPos.y)
            {
                float amountOfSecondsForNext = UtilityManager.SINGLETON.RandomNumbersInterval(0.5f, 1.5f);
                CreateAnotherBridge();
            }
        }
      yield return null;
    }

    void CreateAnotherBridge()
    {
        float speed = UtilityManager.SINGLETON.RandomNumbersInterval(0.3f, 0.8f);
        Vector3 tallestWallPosition = UtilityManager.SINGLETON.SearchHighestGameObjectY("bridges");

        //TODO: maybe use UtilityManager for it
        int bridgeType = Random.Range(0, 2) == 0 ? 1 : 2;
        
        if(bridgeType == 1)
        {
            InstantiateBridge(BridgeType.MOVING, tallestWallPosition, speed);
        }
        
        if(bridgeType == 2)
        {
            InstantiateBridge(BridgeType.ROTATING, tallestWallPosition, speed);
        }
   }

    void InstantiateBridge(BridgeType type, Vector3 tallestWallPosition, float speed)
    {
        Debug.Log("Creating bridge type: " + type + "| on position: " + tallestWallPosition);

        switch (type)
        {
            case BridgeType.MOVING:
                GameObject mBridge = Instantiate(movingBridgePrefab, new Vector3(movingBridgePrefab.transform.position.x, tallestWallPosition.y, movingBridgePrefab.transform.position.z) 
                                                                    + new Vector3(0.0f, bridgeGap, 0.0f), transform.rotation) as GameObject;
                mBridge.GetComponent<MovingBridge>().speedClosing = speed;
                break;

            case BridgeType.ROTATING:
                GameObject rBridge = Instantiate(rotatingBridgePrefab, new Vector3(rotatingBridgePrefab.transform.position.x, tallestWallPosition.y, rotatingBridgePrefab.transform.position.z) 
                                                                     + new Vector3(0.0f, bridgeGap, 0.0f), transform.rotation) as GameObject;
                rBridge.GetComponent<RotatingBridge>().speedClosing = speed;
                break;
        }
    }
}
#endregion