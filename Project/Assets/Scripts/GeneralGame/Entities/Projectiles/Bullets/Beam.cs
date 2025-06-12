using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using MyTypes;
using UnityEngine;
using UnityEngine.UI;

public class Beam : Bullet
{
    protected Timer atkSpeed = TimeManager.Instance.CreateTimer(0.1f);
    protected Timer lifeTimer = TimeManager.Instance.CreateTimer(1);
    protected override void AddAwake()
    {
        base.AddAwake();
        Speed = new PointStruct(0);
    }

    bool isAttack = false;

    protected override void AddFixedUpdate()
    {        
        float distance = lastDelta.magnitude;
        int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
        for (int i = 0; i < count; i++)
        {
            Entity doll = hitBuffer[i].collider.gameObject.GetComponent<Entity>();
            if (doll != null)
            {
                if(atkSpeed.IsTime)
                    isAttack = true;
                if (isAttack)
                {
                    doll.Collide();
                    OnCollide(doll.gameObject);
                }
            }
        }
        isAttack = false;
        lastDelta = move;
        transform.localScale = new Vector3(1,Mathf.Clamp(1 - lifeTimer.GetRatio(),0,1), 1); ;
    }



    Beam beam = null;
    public override void Shot(Transform parent, Vector2 direction)
    {
        base.Shot(parent, direction);
        if (beam == null)
        {
            beam = (Beam)Instantiate(gameObject, parent)
                 .GetComponent<PhysicsBullet>();
            beam.transform.position = parent.transform.position; 
            Destroyer.Instance.Destroy(beam.gameObject, lifeTimer);
            beam.Dir = direction;
            beam.Speed = new PointStruct(0);
        }
        else
            beam.lifeTimer.Reset();
        //return beam.gameObject;
    }


    protected override void OnCollide(GameObject otherGameObject)
    {
        
    }
}
