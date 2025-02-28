using Assets.Scripts.GeneralGame.Entities.Creatures;
using UnityEngine;

public class Entity : MonoBehaviour, IPhysical
{
    protected Rigidbody2D rb2d;

    [SerializeField]
    protected LayerMask layer;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(!rb2d)
            rb2d= gameObject.AddComponent<Rigidbody2D>();
        gameObject.layer = layer;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        rb2d.useFullKinematicContacts = false;
    }


    public virtual void Collide()
    {
        
    }
}
