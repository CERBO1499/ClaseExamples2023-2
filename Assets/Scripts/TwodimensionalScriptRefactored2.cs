using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwodimensionalScriptRefactored2 : MonoBehaviour
{
    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float deceleration = 2.0f;
    [SerializeField] private float maxWalkVel = 0.5f;
    [SerializeField] private float maxRunVel = 2.0f;

    private Animator _anim;
    private float _velocityZ = 0.0f;
    private float _velocityX = 0.0f;

    private int _velocityZHash;
    private int _velocityXHash;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _velocityXHash = Animator.StringToHash("Velocity X");
    }

    private void Update()
    {
        HandleMovementInput();
        UpdateAnimator();
    }

    private void HandleMovementInput()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maxRunVel : maxWalkVel;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, currentMaxVelocity,runPressed);
    }

    private void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity, bool runPressed)
    {
        float targetVelocityZ = forwardPressed ? currentMaxVelocity : 0;
        float targetVelocityX = 0;

        if (rightPressed)
        {
            targetVelocityX = currentMaxVelocity;
        }
        else if (leftPressed)
        {
            targetVelocityX = -currentMaxVelocity;
        }

        float accelerationValue = runPressed ? acceleration : deceleration;

        _velocityZ = Mathf.MoveTowards(_velocityZ, targetVelocityZ, accelerationValue * Time.deltaTime);
        _velocityX = Mathf.MoveTowards(_velocityX, targetVelocityX, accelerationValue * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        _anim.SetFloat(_velocityZHash, _velocityZ);
        _anim.SetFloat(_velocityXHash, _velocityX);
    }
}