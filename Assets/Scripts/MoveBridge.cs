using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Move");
            button.SetActive(false);
        }
    }
}
