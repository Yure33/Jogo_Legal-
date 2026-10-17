using UnityEngine;

public class TestePlayer : MonoBehaviour
{
    [SerializeField] private float _vel = 10f;
    [SerializeField] private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocityX = _vel;
    }
}
