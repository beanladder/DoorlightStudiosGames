using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class JogAndRunAnimationController : MonoBehaviour
{
    [SerializeField]float accleration = 2f;
    [SerializeField]float decleration = 2f;
    [SerializeField]float velocityX = 0.0f;
    [SerializeField]float velocityZ = 0.0f;
    [SerializeField]float maxRunVelocity = 2.0f;
    [SerializeField]float maxJogVelocity = 0.5f;

    Animator anim;
    void Awake(){
        anim = GetComponent<Animator>();
    }
    void Update(){
        bool forward = Input.GetKey("w");
        bool back = Input.GetKey("s");
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        bool run = Input.GetKey("left shift");
        float currentMaxVelocity = run? maxRunVelocity: maxJogVelocity;
        if(forward && velocityZ<currentMaxVelocity){
            velocityZ+= Time.deltaTime*accleration;
        }
        if(back && velocityZ>-currentMaxVelocity){
            velocityZ-= Time.deltaTime*accleration;
        }
        if(left && velocityX>-currentMaxVelocity){
            velocityX-=Time.deltaTime*accleration;
        }
        if(right && velocityX<currentMaxVelocity){
            velocityX+=Time.deltaTime*accleration;
        }
        if(!forward && velocityZ>currentMaxVelocity){
            velocityZ-=Time.deltaTime*decleration;
        }
        if(!back && velocityZ<-currentMaxVelocity){
            velocityZ+=Time.deltaTime*decleration;
        }
        if(!left && velocityX<-currentMaxVelocity){
            velocityX+=Time.deltaTime*decleration;
        }
        if(!right && velocityX>currentMaxVelocity){
            velocityX-=Time.deltaTime*decleration;
        }

        anim.SetFloat("VelocityX",velocityX);
        anim.SetFloat("VelocityZ",velocityZ);
    }
}
