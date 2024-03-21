using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float jumpTime;
    private float jumpTimeCounter;

    private bool isGrounded;

    public Transform groundCheckPos;

    public LayerMask groundMask;

    private Vector2 inputDirection;

    [SerializeField]
    private PlayerController playerController;

    void Update()
    {
        CheckGround();
        InputControlJump();
    }

    void FixedUpdate()
    {
        InputControlVector();
    }

    // 캐릭터에게 입력벡터를 전달
    private void InputControlVector()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        inputDirection = new Vector2(moveX, 0);
        playerController.Move(inputDirection);
    }

    private void InputControlJump()
    {
        // 점프 트리거
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            jumpTimeCounter = jumpTime;
            playerController.Jump(Vector2.up);
        }

        // 점프 파워 가중
        if (Input.GetKey(KeyCode.UpArrow) && !isGrounded)
        {
            if (jumpTimeCounter > 0)
            {
                playerController.Jump(Vector2.up);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // 점프 파워 가중 - 중복 점프 버그 수정
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpTimeCounter = 0;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundMask);
    }
}
