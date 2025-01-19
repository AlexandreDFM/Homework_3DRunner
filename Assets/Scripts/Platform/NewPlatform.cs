using UnityEngine;

public class NewPlatform : MonoBehaviour
{
    public Transform player;
    public GameObject platform;
    public Transform limitSpawnNewPlatform;
    public Transform limitDestroyOldPlatform;
    public Transform summonPlatformPoint;
    [HideInInspector] public GameObject oldPlatform;

    private bool _isPlatformCreated = false;

    private void Update()
    {
        if (!oldPlatform && !_isPlatformCreated && (player.transform.position.z >= limitSpawnNewPlatform.position.z)) {
            // Create new Platform
            var newPosition = summonPlatformPoint.position;
            var newRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            var newPlatform = Instantiate(platform, newPosition, newRotation);
            newPlatform.GetComponent<NewPlatform>().oldPlatform = gameObject;
            _isPlatformCreated = true;
        } else if (_isPlatformCreated && player.transform.position.z >= limitDestroyOldPlatform.position.z) {
            // Destroy the old Platform
            Destroy(gameObject);
        }
    }

}

