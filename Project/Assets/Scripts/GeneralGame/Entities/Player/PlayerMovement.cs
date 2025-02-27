using Assets.Scripts.GeneralGame.Entities.Player;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput input;

    [SerializeField]
    private float speed = 1.0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();    
    }

    private void Update()
    {
        var dir = input.Direction;
        transform.position += new Vector3(dir.x,dir.y,0) * speed * 0.1f;
    }
}
