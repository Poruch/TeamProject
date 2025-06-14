using Assets.Scripts.Accessory;
using UnityEngine;
using MyTypes;
using System.Collections.Generic;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame;
using Assets.Scripts.GeneralGame.Entities.Projectiles;

/// <summary>
/// Класс для объекта летящего в 1 сторону, который может сталкиваться с другими сущностями
/// </summary>
public class PhysicsBullet : Moveable
{
    [SerializeField]
    LayerMask mask;

    [SerializeField]
    protected float shellRadius = 0.01f;

    [SerializeField]
    protected ContactFilter2D contactFilter;


    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected Vector2 lastDelta = Vector2.zero;

    protected override void AddAwake()
    {
        contactFilter.SetLayerMask(mask);
        contactFilter.useLayerMask = true;        
    }


    /// <summary>
    /// Просчет столкновений
    /// </summary>
    protected override void AddFixedUpdate()
    {
        float distance = lastDelta.magnitude;        
        int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
        for (int i = 0; i < count; i++)
        {
            Entity doll = hitBuffer[i].collider.gameObject.GetComponent<Entity>();
            if (doll != null)
            {
                doll.Collide();
                OnCollide(doll.gameObject);

            }
        }
        lastDelta = move;
    }
    public void SetContact(LayerMask newMask)
    {
        contactFilter.SetLayerMask(newMask);
    }

    protected virtual void OnCollide(GameObject otherGameObject)
    {
        
    }
}
