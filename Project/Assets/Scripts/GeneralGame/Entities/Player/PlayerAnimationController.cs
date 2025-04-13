using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class PlayerAnimationController : MonoBehaviour
    {
        Animator animator;        
        SpriteRenderer spriteRenderer;

        public void SetConfig(PlayerConfig config)
        {
            animator = gameObject.AddComponent<Animator>();

            animator.runtimeAnimatorController = config.Animator;
            spriteRenderer.sprite = config.Sprite;


            for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
                
                AnimationEvent animationStartEvent = new AnimationEvent();
                animationStartEvent.time = 0;
                animationStartEvent.objectReferenceParameter = this;
                animationStartEvent.functionName = "AnimationStartHandler";
                animationStartEvent.stringParameter = clip.name;

                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.objectReferenceParameter = this;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = clip.name;

                clip.AddEvent(animationStartEvent);
                clip.AddEvent(animationEndEvent);
            }

        }
        public void AnimationStartHandler(string name)
        {
            Debug.Log($"{name} animation start.");
        }
        public void AnimationCompleteHandler(string name)
        {
            Debug.Log($"{name} animation complete.");
            OnDeath.Invoke();
        }


    }
}
