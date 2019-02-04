using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int m_GameScore = 0;

    #region SINGLETON
    private static ScoreManager _instance = null;

    public static ScoreManager SINGLETON
    {
        get
        {
            if (_instance == null)
            {
#if (DEBUG)
                Debug.LogError("[ScoreManager] Service did not start!!");
#endif
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

    // Use this for initialization
    void Start()
    {
        if (_instance != null)
        {
#if (DEBUG)
            Debug.Log("[ScoreManager] ScoreManager started");
#endif
        }
        else
        {
#if (DEBUG)
            Debug.LogError("[ScoreManager] ScoreManager failed!");
#endif
        }
    }

    void OnDestroy()
    {
        _instance = null;
    }

    #region CUSTOM_METHODS
    public void AddScore(int score)
    {
        m_GameScore += score;
        OnAddScoreText();
    }

    private void OnAddScoreText()
    {
        Debug.Log(m_GameScore);
    }
    #endregion

}
