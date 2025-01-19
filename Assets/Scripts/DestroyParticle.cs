using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy the particle system after 2 seconds
        Destroy(gameObject, 2.0f);
    }
}
