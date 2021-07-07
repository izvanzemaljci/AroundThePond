using UnityEngine;
using System.Collections;

public class LeafController : MonoBehaviour
{
    private Rigidbody rb;
    
    private void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        float turn = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * 2.0f * turn);
    }
}
