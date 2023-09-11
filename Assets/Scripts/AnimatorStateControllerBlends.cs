using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateControllerBlends : MonoBehaviour
{
    private Animator anim;
    private float velocity;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    private int VelocityHash;

    private void Start()
    {
        anim = GetComponent<Animator>();

        VelocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (forwardPressed)
        {
            velocity += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        anim.SetFloat(VelocityHash,velocity);
    }
}
