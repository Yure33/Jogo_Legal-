using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoDestroyAnimation : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        
        Destroy(gameObject, stateInfo.length);
    }
}