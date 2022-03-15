using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameController>().LooseLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.I.Play("Splash");
            //TODO:
        }
    }
}
