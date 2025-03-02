using UnityEngine;


/// <summary>
/// Класс для всех физических объектов на сцене
/// </summary>
public class Entity : MonoBehaviour
{

    protected Rigidbody2D rb2d;
    
    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(!rb2d)
            rb2d= gameObject.AddComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        rb2d.useFullKinematicContacts = false;
    }

    public virtual void Collide()
    {
        
    }

    
}
