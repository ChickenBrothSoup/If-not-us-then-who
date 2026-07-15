using UnityEngine;

public class FodderHurtBoxScript : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ally"))
        {
            Destroy(other.gameObject);
        }
    }
}
