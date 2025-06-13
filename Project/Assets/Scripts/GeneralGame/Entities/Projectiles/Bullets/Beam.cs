using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using MyTypes;
using System.Collections.Generic;
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

    public Beam CreateBeam(Transform parent, Vector2 position, Vector2 direction, Quaternion quaternion)
    {
        var beam = (Beam)Instantiate(gameObject, parent).GetComponent<PhysicsBullet>();
        beam.transform.position = new Vector2(parent.position.x, parent.position.y) + position;
        Destroyer.Instance.Destroy(beam.gameObject, lifeTimer);
        beam.Dir = direction;
        return beam;
    }
    class BeamGun : Gun
    {
        Beam beam = null;
        Beam construct;
        public BeamGun(Beam beam)
        {
            construct = beam;
        }
        public override Bullet[] Shot(Transform parent, Vector2 position, Vector2 direction, Quaternion quaternion)
        {
            if (beam == null)
            {
                beam = construct.CreateBeam(parent, position, direction, quaternion);   
            }
            else
                beam.lifeTimer.Reset();
            return new Bullet[] { beam };
        }
    }

    public override Gun GetGun()
    {
        return new BeamGun(this);
    }



    protected override void OnCollide(GameObject otherGameObject)
    {
        
    }
}
