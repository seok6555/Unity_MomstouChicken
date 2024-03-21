using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector2 inputDirection)
    {
        rigidbody.velocity = new Vector2(inputDirection.x * moveSpeed, rigidbody.velocity.y);
    }

    public void Jump(Vector2 inputJump)
    {
        rigidbody.velocity = inputJump * jumpForce;
    }
}
