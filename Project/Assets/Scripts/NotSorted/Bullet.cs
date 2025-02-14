using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 dir;

    public Vector2 Dir { get => dir; set => dir = value; }

    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position += new Vector3(Dir.x, Dir.y, 0) * 0.2f;
    }
}
