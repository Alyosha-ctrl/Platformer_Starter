using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDriver : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    public float groundAcceleration = 15f;

    public float jumpHeight = 2.5f;

    public float apexTime = .5f;

    public float coyoteTime = .1f;
    public bool coyote = false;

    public GameObject gameManager;
    
    Vector2 _velocity;

    Animator _animator;

    Quaternion _facingRight;
    Quaternion _facingLeft;

    CharacterController _controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();

        _facingRight = Quaternion.Euler(0f, 90f, 0f);
        _facingLeft = Quaternion.Euler(0f, -90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0f;

        if(Keyboard.current.dKey.isPressed) direction += 5f;
        if(Keyboard.current.aKey.isPressed) {
            direction -= 5f;
        }
        bool jumpPressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool jumpHeld = Keyboard.current.spaceKey.isPressed;

        if (jumpPressedThisFrame && (_controller.isGrounded || coyote))
        {
            _velocity.y = 2f*jumpHeight/apexTime;
            // if (coyote)
            // {
            //     Debug.Log("Coyote'd it");
            // }
            // coyote = false;
            Invoke("resetCoyote", coyoteTime);
        }
        else if (_controller.isGrounded)
        {
           coyote = true; 
        }
        else
        {
            float grav =1f;

            if(jumpHeld) grav *= .5f;

            _velocity.y -= grav;
        }

        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.up, out hit, 2f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("brick"))
        {
            Destroy(hit.collider.gameObject);
            gameManager.GetComponent<script>().addScore(100);
        }
        if (hit.collider != null && hit.collider.gameObject.CompareTag("question"))
        {
            Debug.Log("Get Coin");
            gameManager.GetComponent<script>().addCoin();
        }

        if(direction != 0)
        {
            _velocity.x += groundAcceleration*Time.deltaTime*direction;
            _velocity.x = Mathf.Clamp(_velocity.x, -walkSpeed*1.5f, walkSpeed*1.5f);
            transform.rotation = (direction > 0f) ? _facingRight: _facingLeft;
        }
        else
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0f, groundAcceleration);
        }

        


        float deltaX = _velocity.x*Time.deltaTime;
        float deltaY = _velocity.y*(Time.deltaTime);
        Vector3 deltaPosition = new(deltaX, deltaY, 0f);
        CollisionFlags collisionFlags = _controller.Move(deltaPosition);

        // Reset velocities based on collisions
        if ((collisionFlags & CollisionFlags.Above) != 0 && _velocity.y > 0f)
        _velocity.y = 0f;
        if ((collisionFlags & CollisionFlags.Sides) != 0)
        _velocity.x = 0f;

        _animator.SetFloat("speed", Math.Abs(_velocity.x));
    }

    void resetCoyote()
    {
        coyote = false;
    }
}
