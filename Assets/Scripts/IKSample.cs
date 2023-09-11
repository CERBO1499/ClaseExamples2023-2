using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSample : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (!ikActive) return;
            if (lookObj != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookObj.position);
            }

            if (rightHandObj == null) return;
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
            animator.SetLookAtWeight(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
