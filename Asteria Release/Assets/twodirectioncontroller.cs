using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twodirectioncontroller : MonoBehaviour
{
    Animator animator;
    float velocityx = 0.0f;
    float velocityz = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftpressed = Input.GetKey("a");
        bool rightpressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        
        if (forwardPressed && velocityz < 0.5f && !runPressed)
        {
            velocityz += Time.deltaTime * acceleration;
        }
        if (leftpressed && velocityx > 0.5f && !runPressed)
        {
            velocityx -=Time.deltaTime * acceleration;
        }
        if (rightpressed && velocityx < 0.5f && !runPressed)
        {
            velocityx += Time.deltaTime * acceleration;
        }
        if (!forwardPressed && velocityz > 0.0f)
        {
            velocityz -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocityz < 0.0f)
        {
            velocityz = 0.0f;
        }
        if (!leftpressed && velocityx < 0.0f)
        {
            velocityx += Time.deltaTime * deceleration;
        }
        if (!rightpressed && velocityx < 0.0f)
        {
            velocityx += Time.deltaTime * deceleration;
        }
        if (!leftpressed && !rightpressed && velocityx != 0.0f && (velocityx > -0.05f && velocityx < 0.05f))
        {
            velocityx = 0.0f;
        }

        animator.SetFloat("Velocity z", velocityz);
        animator.SetFloat("Velocity x", velocityx);
    }
}
