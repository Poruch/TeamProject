using Assets.Scripts.Accessory;
using UnityEngine;
using MyTypes;
using System.Collections.Generic;
using Assets.Scripts.GeneralGame.Entities.Creatures;

/// <summary>
/// Класс для объекта летящего в 1 сторону, который может сталкиваться с другими сущностями
/// </summary>
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

    /// <summary>
    /// Просчет столкновений
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 move = dir;
        float distance = move.magnitude;
        int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
        for (int i = 0; i < count; i++)
        {
            Entity doll = hitBuffer[i].collider.gameObject.GetComponent<Entity>();
            if (doll != null)
            {
                doll.Collide();
                Destroyer.Instance.Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Движение
    /// </summary>
    private void Update()
    {
        transform.position += new Vector3(dir.x, dir.y, 0) * 0.2f;
    }
}
