using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float mulitdash;
    float _xInput;
    bool _isDash;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField]bool canMove;

    public void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        float xVelocity = _xInput * moveSpeed;
        if (_isDash)
        {
            xVelocity *= mulitdash;
        }
        _rb.linearVelocityX = xVelocity;

        var p = _rb.position;
        p.x = Mathf.Clamp(p.x, minX, maxX);
        _rb.position = p;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _xInput = context.ReadValue<float>();
    }

    public void Dash(InputAction.CallbackContext context)
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

    public void SetCanmove(bool canMove)
    {
        
    }

    public void ClampPosition()
    {
        
    }
}
