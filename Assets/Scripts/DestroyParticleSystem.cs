using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
