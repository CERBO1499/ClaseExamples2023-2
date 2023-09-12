using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IKSample2 : MonoBehaviour
{
    private Animator _animator;

    [Range(0, 1f)] public float distanceToGround;
    [SerializeField] private LayerMask nonPlayer;
    [SerializeField] private bool useCurves;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool("walk", !_animator.GetBool("walk"));
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator)
        {
            //Left Foot
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,useCurves?_animator.GetFloat("IKLeftFoot"):1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,useCurves?_animator.GetFloat("IKLeftFoot"):1);

            RaycastHit hit;
            Ray ray = new Ray(_animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToGround + 1f,nonPlayer))
            {
                if (hit.transform.tag ==("Walkable"))
                {
                    Vector3 footposition = hit.point;
                    footposition.y += distanceToGround;
                    _animator.SetIKPosition(AvatarIKGoal.LeftFoot,footposition);
                    _animator.SetIKRotation(AvatarIKGoal.LeftFoot,Quaternion.LookRotation(transform.forward,hit.normal));
                }
            }
            
            //Rigth Foot
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,useCurves?_animator.GetFloat("IKRightFoot"):1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,useCurves?_animator.GetFloat("IKRightFoot"):1);

           ray = new Ray(_animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToGround + 1f,nonPlayer))
            {
                if (hit.transform.tag == ("Walkable"))
                {
                    Vector3 footposition = hit.point;
                    footposition.y += distanceToGround;
                    _animator.SetIKPosition(AvatarIKGoal.RightFoot,footposition);
                    _animator.SetIKRotation(AvatarIKGoal.RightFoot,Quaternion.LookRotation(transform.forward,hit.normal));
                }
            }
        }
    }
}