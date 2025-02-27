using Assets.Scripts.Accessory;
using UnityEngine;
using MyTypes;


public class Bullet : MonoBehaviour
{
    Vector2 dir = Vector2.right;

    private void Update()
    {
        transform.position += new Vector3(dir.x,dir.y,0) * 0.2f;
    }
    private void Start()
    {
        Destroyer.Instance.Destroy(gameObject,new Timer(10));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        Doll doll = collision.gameObject.GetComponent<Doll>();
        if (doll)
        {
            doll.Attacked();
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        
    }
}
