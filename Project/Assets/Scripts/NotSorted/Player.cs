using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.up * 0.1f;
        else if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.down * 0.1f;
        else if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * 0.1f;
        else if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * 0.1f;
    }
}
