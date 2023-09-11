using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        IdleBreaker,
        Walk,
        Run
    }

    private PlayerState _currentState;
    public Animator _characterAnimator;
    
    void Start()
    {
        SetState(PlayerState.Idle);
    }

    void Update()
    {
        PlayerState newState = DetermineStateFromInput();
        if (newState != _currentState)
            SetState(newState);
    }

    private PlayerState DetermineStateFromInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return PlayerState.IdleBreaker;
        }
        else if (IsRunning())
        {
            return PlayerState.Run;
        }
        else if (IsWalking())
        {
            return PlayerState.Walk;
        }
        else
        {
            return PlayerState.Idle;
        }
    }

    private void SetState(PlayerState newState)
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", false);
                break;

            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", false);
                break;

            case PlayerState.Walk:
                _characterAnimator.SetBool("Walk", false);
                _characterAnimator.SetBool("IsWalking", false);
                break;

            case PlayerState.Run:
                _characterAnimator.SetBool("Run", false);
                _characterAnimator.SetBool("IsRunning", false);
                break;
        }

        switch (newState)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", true);
                break;

            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", true);
                break;

            case PlayerState.Walk:
                _characterAnimator.SetBool("Walk", true);
                _characterAnimator.SetBool("IsWalking", true);

                break;

            case PlayerState.Run:
                _characterAnimator.SetBool("Run", true);
                _characterAnimator.SetBool("IsRunning", true);

                break;
        }

        _currentState = newState;
    }
    
    private bool IsWalking()
    {
        return Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift);
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift);
    }
}