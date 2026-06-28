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
        if (_rb == null)
        {
            return;
        }

        if (!canMove)
        {
            _rb.linearVelocityX = 0f;
            return;
        }

        float xVelocity = _xInput * moveSpeed;
        if (_isDash)
        {
            xVelocity *= mulitdash;
        }
        _rb.linearVelocityX = xVelocity;

        ClampPosition();
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
        this.canMove = canMove;

        if (!canMove && _rb != null)
        {
            _rb.linearVelocityX = 0f;
        }
    }

    public void ClampPosition()
    {
        if (_rb == null)
        {
            return;
        }

        Vector2 position = _rb.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        _rb.position = position;
    }
}
