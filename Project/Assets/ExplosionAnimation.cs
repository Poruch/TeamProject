using Assets.Scripts.Accessory;
using UnityEngine;
public class ExplosionAnimation : StateMachineBehaviour
{


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroyer.Instance.Destroy(animator.gameObject);
    }

}
