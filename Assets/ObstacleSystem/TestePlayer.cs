using UnityEngine;

public class TestePlayer : MonoBehaviour
{
    [SerializeField] private float _vel = 10f;
    public float Vel {get => _vel; set => _vel = value; }
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float resistency = 5.0f;
    public float Resistency { get => resistency; set => resistency = value; }

    void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
    }

    public void SlowDown(float slowFactor)
    {
        _rb.linearVelocity *= slowFactor;
    }

    public void Repulse(Vector2 collisionNormal, float repulseForce)
    {
        Vector2 directionReflected = Vector2.Reflect(_rb.linearVelocity.normalized, collisionNormal);
        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(directionReflected * repulseForce, ForceMode2D.Impulse);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocityX = _vel;
    }
}
