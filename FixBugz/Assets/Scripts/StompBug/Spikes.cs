using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            PlayerHealth_StompBug.Instance.UpdateHealth(-1);
        }
    }
}