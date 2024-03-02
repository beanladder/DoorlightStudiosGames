using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator anim;
    float velocityX = 0.0f, velocityZ = 0.0f;
    public float accleration = 2.0f;
    public float decleration = 2.0f;
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

        if(forwardPressed && velocityZ<0.5f)
        {
            velocityZ += Time.deltaTime * accleration;
        }

        if(backPressed && velocityZ > -0.5f)
        {
            velocityZ -= Time.deltaTime * accleration;
        }

        if(leftPressed&& velocityX>-0.5f)
        {
            velocityX -= Time.deltaTime * accleration;
        }

        if(rightPressed && velocityX<0.5f)
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

        if(backPressed && forwardPressed)
        {
            if(velocityZ>0.0f)
            {
                velocityZ -= Time.deltaTime * decleration;
            }
            if (velocityZ < 0.0f)
            {
                velocityZ += Time.deltaTime * accleration;
            }
            
        }


        if(leftPressed && rightPressed)
        {
            if(velocityX>0.0f)
            {
                velocityX -= Time.deltaTime * decleration;
            }
            if (velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * accleration;
            }
        }

        anim.SetFloat("VelocityZ", velocityZ);
        anim.SetFloat("VelocityX",velocityX);
    }
}