using System;
using Unity.Cinemachine;
using UnityEngine;

public class BolaFogo_Script : MonoBehaviour
{
    public static event Action fimDePartida;
    public float Vida = 100;
    [SerializeField] int PerdaChao;
    [SerializeField] Rigidbody2D fisica;
    [SerializeField] int[] qntPerIndex;
    [SerializeField] CinemachineImpulseSource impulso;
    public Vector2 Direction;
    public float Velocity = 1;
    
    void Start()
    {
        fisica.AddForce(Direction*Velocity, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D cool)
    {
        Debug.Log("Bati");
        impulso.GenerateImpulse();
        Vida -= PerdaChao;
        fisica.linearVelocityX /= 1.25f;
        fisica.AddForce(Vector2.up*fisica.linearVelocity.magnitude/1.3f, ForceMode2D.Impulse);
        if(Vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        fimDePartida?.Invoke();
    }
}
