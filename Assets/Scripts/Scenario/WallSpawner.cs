using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Util;

public class WallSpawner : MonoBehaviour {

    public GameObject wallPrefab;
    public GameObject StupidCube;
    public float offsetToBuild;

    private float StupidCubeHeight;

	void Start () {
        StupidCubeHeight = 0.0f;
	}
	
	void Update () {
        CheckIfNeedToBuildWalls();
	}

    void CheckIfNeedToBuildWalls()
    {
        Vector3 tallestWall = UtilityManager.SINGLETON.SearchHighestGameObjectY("lateralWalls");
        StupidCubeHeight = StupidCube.transform.position.y;

        if (StupidCubeHeight + offsetToBuild > tallestWall.y)
        {
            GameObject wall = Instantiate(wallPrefab, tallestWall + new Vector3(0.0f, 80.0f, 0.0f), transform.rotation) as GameObject;
        }
    }
}
