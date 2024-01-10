using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementAnimation : MonoBehaviour


{
    Animator animator;
    int IsWalkingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        IsWalkingHash = Animator.StringToHash("IsMoving");
    }

    // Update is called once per frame
    void Update()
    {
        bool IsWalking = animator.GetBool(IsWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        if (Input.GetKey("w"))
        {
            animator.SetBool("IsMoving", true);
        }
        if (IsWalking && !forwardPressed)
        {
            animator.SetBool(IsWalkingHash, false);
        }
    }
}
