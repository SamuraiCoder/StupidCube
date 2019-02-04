using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridge : MonoBehaviour {

    public float speedClosing;
    public GameObject bridgeRight;
    public GameObject bridgeLeft;

    private Animator animRight;
    private Animator animLeft;

	void Start () {
        animRight = bridgeRight.GetComponent<Animator>();
        animLeft = bridgeLeft.GetComponent<Animator>();

        animRight.speed = speedClosing;
        animLeft.speed = speedClosing;
	}
}
