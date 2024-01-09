using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] Item[] items;


    int itemIndex;
    int previousItemIndex = -1;

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
        if (PV.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;

        Look();
        Move();
        Jump();
        for(int i = 0; i < items.Length; i++)
		{
			if(Input.GetKeyDown((i + 1).ToString()))
			{
				EquipItem(i);
				break;
			}
		}
        
    }

    void EquipItem(int _index)
	{

		itemIndex = _index;

		items[itemIndex].itemGameObject.SetActive(true);

        if(previousItemIndex != -1)
		{
			items[previousItemIndex].itemGameObject.SetActive(false);
		}

		previousItemIndex = itemIndex;

	}
/*
    void UpdateCrosshairVisibility()
    {
        bool cursorVisible = Cursor.lockState == CursorLockMode.None;
        crosshair.SetVisibility(!cursorVisible); // Set crosshair visibility based on cursor lock state
    }

    void SwitchGun()
    {
        // Check if the gun needs to be switched using keys "Q" and "E"
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToPreviousGun();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchToNextGun();
        }
    }

    void SwitchToNextGun()
    {
        SetGunOffsets(currentGunIndex);

        if (currentGun != null)
        {
            PhotonNetwork.Destroy(currentGun);
        }

        currentGunIndex = (currentGunIndex + 1) % numberOfGuns;
        InstantiateGun();
    }

    void SwitchToPreviousGun()
    {
        SetGunOffsets(currentGunIndex);

        if (currentGun != null)
        {
            PhotonNetwork.Destroy(currentGun);
        }

        currentGunIndex = (currentGunIndex - 1 + numberOfGuns) % numberOfGuns;
        InstantiateGun();
    }
    Vector3 gunPositionOffset = Vector3.zero;
    Vector3 gunRotationOffset = Vector3.zero;

    void SetGunOffsets(int index)
    {
        switch (index)
        {
            case 0:
                gunPositionOffset = gun1PositionOffset;
                gunRotationOffset = gun1RotationOffset;
                break;
            case 1:
                gunPositionOffset = gun2PositionOffset;
                gunRotationOffset = gun2RotationOffset;
                break;
            // Add cases for more guns if needed
        }
    }

    void InstantiateGun()
    {
        Vector3 gunPositionOffset = Vector3.zero;
        Vector3 gunRotationOffset = Vector3.zero;

        SetGunOffsets(currentGunIndex);

        currentGun = PhotonNetwork.Instantiate(
            currentGunIndex == 0 ? gun1Prefab.name : gun2Prefab.name,
            transform.position + gunPositionOffset,
            Quaternion.Euler(gunRotationOffset)
        );

        currentGun.transform.parent = transform;
        currentGun.GetComponent<PhotonView>().TransferOwnership(PV.ViewID);
        PV.RPC("SetGunVisibility", RpcTarget.AllBuffered, true);

        previousGunIndex = currentGunIndex;
    }*/

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
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
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
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
