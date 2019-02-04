using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {

    public GameObject objectToFollow;
    public float smoothTime = 0.2f;
    
    private Vector3 velocity = Vector3.zero;

	void Update () {
        Vector3 targetPosition = objectToFollow.transform.TransformPoint(new Vector3(0, 1, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	}
}
