using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void setAnimator(int index)                  =>  _animator.SetInteger("Value",index);
    
    public void ChangeAnimationState(string newState)  =>   _animator.Play(newState);
    
}
