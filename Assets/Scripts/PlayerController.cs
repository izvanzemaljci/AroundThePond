using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = default;
    [SerializeField]
    private Transform character = default;
    [SerializeField]
    private float speed = 6.0f;
    private float gravity = 9.87f;
    private float verticalSpeed = 0;

    [SerializeField]
    private Transform cameraHolder = default;
    private float mouseSensitivity = 2.0f;
    private float upLimit = -50.0f;
    private float downLimit = 50.0f;

    [SerializeField]
    private Animator animator = default;

    [SerializeField]
    private float jumpHeight = 8.0f;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 move = Vector3.zero;
    private Vector3 originalPosition = new Vector3(367.9f,9.1f,259.7f);

    private void Update() {
        Move();
        Rotate();
        
        if (Input.GetKey(KeyCode.Space))
        {   
            animator.SetBool("isJumping", true);
            Jump();
        } else animator.SetBool("isJumping", false);
        playerVelocity.y = 0;
    }

    private void Jump() {
        playerVelocity = move * 0.65f;
        playerVelocity.y = jumpHeight;
        characterController.slopeLimit = 90.0f;

        playerVelocity.y -= gravity * Time.deltaTime;
        characterController.Move((move + playerVelocity) * Time.deltaTime);
    }

    private void Move() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if(characterController.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        move = transform.forward * -verticalMove + transform.right * -horizontalMove;
        characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        if (move != Vector3.zero) {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(move), 0.15F);
        }

        animator.SetBool("isWalking", verticalMove != 0 || horizontalMove != 0);
    }

    private void Rotate() {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if(currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }

    public void Return() {
        Time.timeScale = 1f;
        characterController.gameObject.transform.position = originalPosition;
    }
}
