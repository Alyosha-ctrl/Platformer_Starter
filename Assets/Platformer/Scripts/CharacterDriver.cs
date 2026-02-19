using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDriver : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    public float groundAcceleration = 15f;

    public float jumpHeight = 2.5f;

    public float apexTime = .5f;

    Vector2 _velocity;

    CharacterController _controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0f;

        if(Keyboard.current.dKey.isPressed) direction += 1f;
        if(Keyboard.current.aKey.isPressed) direction -= 1f;
        bool jumpPressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool jumpHeld = Keyboard.current.spaceKey.isPressed;

        if (jumpPressedThisFrame)
        {
            Debug.Log("SpacePressed");
            _velocity.y = 2f*jumpHeight/apexTime;
        }

        if(direction != 0)
        {
            _velocity.x += groundAcceleration*Time.deltaTime*direction;
            _velocity.x = Mathf.Clamp(_velocity.x, -walkSpeed, walkSpeed);
        }
        else
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0f, groundAcceleration);
        }

        _velocity.y -= .5f;


        float deltaX = _velocity.x*Time.deltaTime;
        float deltaY = _velocity.y*(Time.deltaTime);
        Vector3 deltaPosition = new(deltaX, deltaY, 0f);
        _controller.Move(deltaPosition);
    }
}
