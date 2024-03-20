using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseSim : MonoBehaviour
{
    private bool isPaused = false;
    public void PauseSim(){
        if(isPaused){
            Time.timeScale = 1;
        }else{
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
    }
}
