using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSample2 : MonoBehaviour
{
    [SerializeField] private float forwardForce = 0.2f;
    private Animator _animator;

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
}