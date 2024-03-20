using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportOver : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D other){
        if(other.tag=="boid"){
            Debug.Log("Teleport");
        }   
        other.gameObject.transform.position = (other.gameObject.transform.position * -1);
    }
}
