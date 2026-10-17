using System;
using UnityEngine;

public class BolaFogo_Script : MonoBehaviour
{
    public static event Action fimDePartida;
    [SerializeField] Rigidbody2D fisica;
    [SerializeField] int[] qntPerIndex;
    public Vector2 Direction;
    public float Velocity = 1;
    
    void Start()
    {
        fisica.AddForce(Direction*Velocity, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D cool)
    {
        Debug.Log("Bati");
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        fimDePartida?.Invoke();
    }
}
