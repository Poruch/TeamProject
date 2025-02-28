using Assets.Scripts.Accessory;
using UnityEngine;
using MyTypes;
using System.Collections.Generic;
using Assets.Scripts.GeneralGame.Entities.Creatures;


public class PhysicsBullet : Entity
{
    public Vector2 dir = Vector2.right;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float shellRadius = 0.01f;


    [SerializeField]
    protected ContactFilter2D contactFilter;


    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    private void Start()
    {
        contactFilter.SetLayerMask(mask);
        contactFilter.useLayerMask = true;
        Destroyer.Instance.Destroy(gameObject,new Timer(10));        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnDestroy()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 move = dir;
        float distance = move.magnitude;
        int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
        for (int i = 0; i < count; i++)
        {
            IPhysical doll = hitBuffer[i].collider.gameObject.GetComponent<IPhysical>();
            if (doll != null)
            {
                doll.Collide();
                Destroyer.Instance.Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        transform.position += new Vector3(dir.x, dir.y, 0) * 0.2f;
    }
}
