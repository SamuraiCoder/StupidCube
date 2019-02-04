using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Util
{
    public class UtilityManager : MonoBehaviour
    {
#region SINGLETON
        private static UtilityManager _instance = null;

        public static UtilityManager SINGLETON
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("[UtilityManager] Service did not start!!");
                }
                return _instance;
            }
        }
#endregion

#region UNITY_METHODS
        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        void Start()
        {
            if (_instance != null)
            {
                Debug.Log("[UtilityManager] UtilityManager started");
            }
            else
            {
                Debug.LogError("[UtilityManager] UtilityManager failed!");
            }
        }

        void OnDestroy()
        {
            _instance = null;
        }
#endregion

#region CUSTOM_METHODS
    public Vector3 SearchHighestGameObjectY(string gameObject)
    {
        GameObject[] arr;
        arr = GameObject.FindGameObjectsWithTag(gameObject);
        Vector3 highest = Vector3.zero;
        float y = 0.0f;

        foreach (GameObject go in arr)
        {
            Vector3 currVec = go.transform.position;
            float currY = currVec.y;

            if (currY > y)
            {
                highest = currVec;
            }
        }
        return highest;
    }

    public float RandomNumbersInterval(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public void WaitAndExecute(float timer = 0.0F, Action callBack = null)
    {
        StartCoroutine(WaitAndExecute_INTERNAL(timer, callBack));
    }

    IEnumerator WaitAndExecute_INTERNAL(float timer, Action callBack = null)
    {
        if(timer != null)
        {
            yield return new WaitForSeconds(timer);
        }
        if (callBack != null)
        {
            callBack();
        }
    }
#endregion
    }
}

