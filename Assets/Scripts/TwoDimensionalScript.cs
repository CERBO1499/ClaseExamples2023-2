using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalScript : MonoBehaviour
{
    
    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float deceleration = 2.0f;
    [SerializeField] private float maxWalkVel = 0.5f;
    [SerializeField] private float maxRunVel = 2.0f;


    private Animator _anim;
    private float _velocityZ = 0.0f;
    private float _velocityX = 0.0f;
    
        
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maxRunVel : maxWalkVel;
        
        //Increase speed
        if (forwardPressed && _velocityZ < currentMaxVelocity)
        {
            _velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && _velocityX > -currentMaxVelocity)
        {
            _velocityX -= Time.deltaTime * acceleration;
        }

        if (rightPressed && _velocityX < currentMaxVelocity)
        {
            _velocityX += Time.deltaTime * acceleration;
        }
        
        //Decrease speed
        if (!forwardPressed && _velocityZ > 0.0f)
        {
            _velocityZ -= Time.deltaTime * acceleration;
        }

        if (!forwardPressed && _velocityZ < 0.0f)
        {
            _velocityZ = 0.0f;
        }

        if (!leftPressed && _velocityX < 0.0f)
        {
            _velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPressed && _velocityX > 0.0f)
        {
            _velocityX -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && !rightPressed && _velocityX != 0.0f && (_velocityX > -0.05 && _velocityX < 0.05f))
        {
            _velocityX = 0.0f;
        }

        //Running forward
        if (forwardPressed && runPressed & _velocityZ > currentMaxVelocity)
        {
            _velocityZ = currentMaxVelocity;
        }
        //Decelerated to the maximun walk velocity
        else if (forwardPressed && _velocityZ > currentMaxVelocity)
        {
            _velocityZ -= Time.deltaTime * deceleration;
            //round to the currentMaxVelocity if whitin offset
            if (_velocityZ > currentMaxVelocity && _velocityZ < (currentMaxVelocity + 0.05f))
            {
                _velocityZ = currentMaxVelocity;
            }
        }
        // Round to the currentVelocity if within offset
        else if (forwardPressed && _velocityZ < currentMaxVelocity && _velocityZ > (currentMaxVelocity - 0.05f))
        {
            _velocityZ = currentMaxVelocity;
        }
        
        _anim.SetFloat("Velocity Z", _velocityZ);
        _anim.SetFloat("Velocity X", _velocityX);

    }
}
