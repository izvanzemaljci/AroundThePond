using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            FindObjectOfType<GameController>().LooseLife();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Splash");
            FindObjectOfType<GameController>().PlayVideo();
        }
    }
}
