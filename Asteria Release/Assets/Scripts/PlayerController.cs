using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] GameObject gun1Prefab, gun2Prefab;

    public float x1, y1, z1;
    public float x2, y2, z2;

    private GameObject currentGun;
    private int currentGunIndex = 1;

 
    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(!PV.IsMine)
        {
			Destroy(GetComponentInChildren<Camera>().gameObject);
			Destroy(rb);
        }
        else
        {
            SwitchGun();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine)
            return;
          
        Look();
        Move();
        Jump();
        SwitchGun();

         
    }


    void SwitchGun()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollWheel != 0)
            {
                currentGunIndex = (currentGunIndex + 1) % 2; // Switch between 0 and 1
                Destroy(currentGun); // Destroy the current gun

                // Instantiate the new gun based on the index
                if (currentGunIndex == 0)
                    currentGun = Instantiate(gun1Prefab, transform.position + new Vector3(x1, y1, z1), transform.rotation, transform);
                else
                    currentGun = Instantiate(gun2Prefab, transform.position + new Vector3(x2, y2, z2), transform.rotation, transform);
            }
        }
    void Look()
    {
         transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

         verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
         verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

         cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
         {
            rb.AddForce(transform.up * jumpForce);
         }
    }

  
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    void FixedUpdate()
    {
        if(!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
