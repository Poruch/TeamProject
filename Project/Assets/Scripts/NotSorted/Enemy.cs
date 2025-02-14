using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    private void Awake()
    {
        player = FindFirstObjectByType<Player>().gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null) 
        {
            rb.linearVelocity = (player.transform.position - transform.position).normalized * 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
            player.SetActive(false);
    }
}
