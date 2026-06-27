using UnityEngine;
using UnityEngine.InputSystem;

public class Player_moti : MonoBehaviour
{
    [SerializeField] float _walkSpeed = 3f;
    [SerializeField] float xDash = 2f;
    Rigidbody2D _rb;
    float _xInput;
    bool _isDash;


    public void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        float xVelocity = _xInput * _walkSpeed;
        if (_isDash)
        {
            xVelocity *= xDash;
        }
        _rb.linearVelocityX = xVelocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _xInput = context.ReadValue<float>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isDash = true;
        }
        else if (context.canceled)
        {
            _isDash = false;
        }
    }
}
