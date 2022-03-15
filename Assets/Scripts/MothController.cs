using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothController : MonoBehaviour
{
    private Vector3 pos;
    private int random;
    private ParticleSystem sys;
    private MeshRenderer mesh;

    private void Awake() {
        sys = GetComponentInParent<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
    }

    private void Start() {
        pos = transform.position;
        random = Random.Range(1,3);
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * random) * random + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            AudioManager.I.Play("Sparkle");
            sys.Play();
            mesh.enabled = false;
            FindObjectOfType<GameController>().CollectFly();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            sys.Stop();
            Destroy(gameObject);
        }
    }
}
