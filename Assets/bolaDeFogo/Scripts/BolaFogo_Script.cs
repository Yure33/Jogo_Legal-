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
    [SerializeField] InvestidaScript investida;
    [SerializeField] ParticleSystem particulaHIT;
    public Vector2 Direction;
    public float Velocity = 1;
    float VelocidadeAntY;
    
    void Start()
    {
        fisica.AddForce(Direction*Velocity, ForceMode2D.Impulse);
        impulso.GenerateImpulse();
    }
    void FixedUpdate()
    {
        if(fisica.linearVelocity.magnitude > 0)
        {
            float ang = Mathf.Atan2(fisica.linearVelocity.y, fisica.linearVelocity.x)* Mathf.Rad2Deg;
            fisica.MoveRotation(ang);
        }

        if(fisica.linearVelocityX < 0.2f)
        {
            ReciveDamage(Vida+50);
        }
    }

    void OnCollisionEnter2D(Collision2D cool)
    {
        Debug.Log("Bati");
        if(cool.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.PlaySound("BolaBate");
            if(investida.estaInvestindo)
            {
                fisica.linearVelocityX /= 1.05f;
                fisica.AddForce(2 * Mathf.Abs(fisica.linearVelocityX*1.5f) * Vector2.up, ForceMode2D.Impulse);
            }
            else
            {
                fisica.linearVelocityX /= 1.25f;
                fisica.AddForce(2 * Mathf.Abs(fisica.linearVelocityX) * Vector2.up, ForceMode2D.Impulse);
            }

            ReciveDamage(PerdaChao);
        }
    }

    public void ReciveDamage(float damage)
    {
        if(particulaHIT.isEmitting)
        {
            particulaHIT.Stop();
        }
        particulaHIT.Play();
        
        if(!investida.estaInvestindo)
        {
            Vida -= damage;
        }
        else
        {
            investida.estaInvestindo = false;
        }
        if (impulso != null) impulso.GenerateImpulse();
        if(Vida <= 0 || fisica.linearVelocityX <= 1f)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyModifiers(float speedMod, float heightMod)
    {
        Vector2 currentVelocity = fisica.linearVelocity;
        currentVelocity.x *= speedMod; 
        fisica.linearVelocity = currentVelocity;

        if (heightMod != 0)
        {
            fisica.AddForce(new Vector2(0, heightMod), ForceMode2D.Impulse);
        }
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }

    public void SlowDown(float slowFactor)
    {
        fisica.linearVelocity *= slowFactor;
    }

    public void Repulse(Vector2 collisionNormal, float repulseForce)
    {
        Vector2 directionReflected = Vector2.Reflect(fisica.linearVelocity.normalized, collisionNormal);
        fisica.linearVelocity = Vector2.zero;
        fisica.AddForce(directionReflected * repulseForce, ForceMode2D.Impulse);
    }

    public void Stop()
    {
        fisica.linearVelocity = Vector2.zero;
        fisica.angularVelocity = 0f;
    }

    void OnDestroy()
    {
        fimDePartida?.Invoke();
    }
}
