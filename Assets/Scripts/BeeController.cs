using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private Transform target = default;
    private float speed = 3f;
    private CharacterController controller;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if(target.position.x - transform.position.x < 20f) {
        transform.LookAt(target.position);
        
        Vector3 direction = target.position - transform.position;

        direction = direction.normalized;

        Vector3 velocity = direction * speed;

        controller.Move(velocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            FindObjectOfType<AudioManager>().Play("Bee");
            FindObjectOfType<GameController>().LooseLife();
            Destroy(gameObject);
        }
    }
}
