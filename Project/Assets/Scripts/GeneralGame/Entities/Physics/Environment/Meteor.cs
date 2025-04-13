using Assets.Scripts.Accessory;
using MyTypes;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Creatures.Environment
{
    /// <summary>
    /// Объект метеорита 
    /// </summary>
    internal class Meteor : PhysicsBullet
    {
        protected override void AddFixedUpdate()
        {
            base.AddFixedUpdate();
            transform.Rotate(new Vector3(0, 0, 180) * Time.fixedDeltaTime);
        }
        
        Animator animator;
        protected override void AddAwake()
        {
            base.AddAwake();
            animator = GetComponent<Animator>();
        }
        protected override void OnCollide(GameObject otherGameObject)
        {
            animator.SetBool("IsDestroy", true);
            SetContact(0);
        }
    }
}
