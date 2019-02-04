using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;

public class WaterMovement : MonoBehaviour {

    public float speed;
    public bool shouldMove = false;
    
    void Update () {
        if(shouldMove || GameManager.SINGLETON.GameState == GameState.GAME)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stupidCube"))
        {
            //GameOver state
            Debug.Log("[WaterMovement] water contacted player.");
            GameManager.SINGLETON.GameState = GameState.END;
            shouldMove = false;
        }
    }
}
