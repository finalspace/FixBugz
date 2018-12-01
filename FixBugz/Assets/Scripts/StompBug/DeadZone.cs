using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Debug.Log("deadzzzzz");
            Destroy(other.gameObject);
        }
    }
}