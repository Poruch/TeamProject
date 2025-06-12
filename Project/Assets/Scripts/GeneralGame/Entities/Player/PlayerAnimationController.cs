using UnityEngine;
using UnityEngine.Events;
namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        SpriteRenderer spriteRenderer;

        public void SetConfig(PlayerConfig config)
        {
            animator = gameObject.AddComponent<Animator>();
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();


            animator.runtimeAnimatorController = config.Animator;
            spriteRenderer.sprite = config.Sprite;


            for (int i = 0; i < Animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = Animator.runtimeAnimatorController.animationClips[i];
                
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
        UnityEvent onCompleteDeathAnimation = new UnityEvent();

        public UnityEvent OnCompleteDeathAnimation { get => onCompleteDeathAnimation; set => onCompleteDeathAnimation = value; }
        public Animator Animator { get => animator; set => animator = value; }
        public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }

        public void AnimationStartHandler(string name)
        {
            //Debug.Log($"{name} animation start.");
        }
        public void AnimationCompleteHandler(string name)
        {
            
            if(name == "DestroyShipAnimation" && OnCompleteDeathAnimation != null)
            {
                OnCompleteDeathAnimation.Invoke();
            }
        }
    }
}
