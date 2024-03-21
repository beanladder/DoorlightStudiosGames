using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator anim;
    float velocityX = 0.0f, velocityZ = 0.0f;
    public float accleration = 2.0f;
    public float decleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool bothLeftAndRight = leftPressed&&rightPressed;

        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        if(forwardPressed && velocityZ<currentMaxVelocity)
        {
            if(!backPressed){
                velocityZ += Time.deltaTime * accleration;
            }
            else{
                velocityZ-=Time.deltaTime*decleration;
                if(velocityZ<0){
                    velocityZ=0f;
                }
            }
            
        }

        if (backPressed && velocityZ > -currentMaxVelocity)
        {
        // Check if both forward and backward keys are pressed
            if (!forwardPressed)
            {
                velocityZ -= Time.deltaTime * accleration;
            }
            else
            {
                // Introduce a separate deceleration when both keys are pressed
                velocityZ += Time.deltaTime*decleration;
                if(velocityZ>0f){
                    velocityZ = 0.0f;
                }
            }
        }


        if(leftPressed&& velocityX>-currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * accleration;
        }

        if(rightPressed && velocityX<currentMaxVelocity)
        {
            velocityX += Time.deltaTime * accleration;
        }

        if(!forwardPressed && velocityZ>0.0f)
        {
            velocityZ -= Time.deltaTime * decleration;
        }

        if(!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * decleration;
        }

        if(!leftPressed && velocityX<0.0f)
        {
            velocityX += Time.deltaTime * decleration;
        }

        if(!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * decleration;
        }

        if(!leftPressed && !rightPressed && velocityX !=0.0f &&(velocityX>-0.05f && velocityX <0.05f))
        {
            velocityX = 0.0f;
        }

        if (!backPressed && !forwardPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        

        if(forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if(forwardPressed && velocityZ>currentMaxVelocity)
        {
            velocityZ-= Time.deltaTime * decleration;
            if(forwardPressed && velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if(forwardPressed && velocityZ<currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        //right pressed

        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * decleration;
            if (rightPressed && velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }

        //left pressed

        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * decleration;
            if (leftPressed && velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        

        anim.SetFloat("VelocityZ", velocityZ);
        anim.SetFloat("VelocityX",velocityX);
    }
}
