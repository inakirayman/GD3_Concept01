using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [Header("Movement")]
    public float MoveSpeed;

    public float GroundDrag;

    public float JumpForce;
    public float JumpCooldown;
    public float airMultiplier;
    private bool _isJumping;



    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask GroundMask;
    private bool _isGrounded;

    [Header("References")]
    public Transform Orientation;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;





    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, GroundMask);


        Inputs();
        SpeedClamp();
        //handle drag
        if (_isGrounded)
            _rigidbody.drag = GroundDrag;
        else
            _rigidbody.drag = 0;



    }

    private void FixedUpdate()
    {
        MovePlayer();
    }





    private void Inputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetButton("Jump")&& _isGrounded && !_isJumping)
        {
            _isJumping = true;

            Jump();

            Invoke(nameof(ResetJump), JumpCooldown);
        }


    }

    private void MovePlayer()
    {
        _moveDirection = Orientation.forward* _verticalInput + Orientation.right * _horizontalInput;

        if(_isGrounded)
        _rigidbody.AddForce(_moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);

        else if (!_isGrounded)
            _rigidbody.AddForce(_moveDirection.normalized * MoveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedClamp()
    {
        Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);


        if(velocity.magnitude > MoveSpeed)
        {
            Vector3 newVelocity = velocity.normalized * MoveSpeed;
            _rigidbody.velocity = new Vector3(newVelocity.x, _rigidbody.velocity.y, newVelocity.z);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        _rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _isJumping = false;
    }


}
