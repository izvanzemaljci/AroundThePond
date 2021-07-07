using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        FindObjectOfType<GameController>().GameOver();
    }
}
