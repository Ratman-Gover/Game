using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject playerBody;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float intialBoostStrength = 3f;
    

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private CinemachineBrain VirtualCamera;

    private bool locked = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        VirtualCamera = playerCamera.GetComponent<CinemachineBrain>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }

    void Update()
    {

        Vector3 Instance = playerBody.transform.position;
        float height = playerBody.transform.localScale.y * 0.8f; // Camera height (head)
        playerCamera.transform.position = new Vector3(Instance.x, Instance.y + height, Instance.z);

        if (Input.GetKeyDown(KeyCode.F))
        {
            intialBoostStrength += 0.2f;
            moveDirection = -playerCamera.transform.forward * intialBoostStrength;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            VirtualCamera.enabled = !VirtualCamera.enabled;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (locked)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}