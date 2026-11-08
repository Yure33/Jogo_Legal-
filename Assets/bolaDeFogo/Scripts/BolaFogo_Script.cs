// SISTEMA DE MOVIMENTO DA BOLA DE FOGO
// SISTEMA DE DANO E MORTE DA BOLA DE FOGO
using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class BolaFogo_Script : MonoBehaviour
{
    public static event Action FimDePartida; // EVENTO SIGNIFICANDO QUE A BOLA DE FOGO MORREU
    [Header("Referencias Essenciais")]
    [SerializeField] Rigidbody2D fisica;
    [SerializeField] int[] qntPerIndex; // QNT DE OBSTACULOS DESTRUIDOS
    [SerializeField] CinemachineImpulseSource impulso;
    [SerializeField] InvestidaScript investida;
    [SerializeField] ParticleSystem particulaHIT;
    [SerializeField] BolaFogoSO data;
    [HideInInspector] public Vector2 Direction;
    [Header("Balanceamento")]
    [SerializeField] Vector2 MaxVelocity;
    Tween tween;
    float defaultFixedTime;
    public float Velocity = 1;
    
    void Start()
    {
        // SALVAR FIXED TIME UPDATE
        defaultFixedTime = Time.fixedDeltaTime;
        // DEFINIR OBJETOS DESTRUIDOS COMO 0
        qntPerIndex = new int[6];
        // APLICAÇÃO DO IMPULSO NA DIREÇÃO OBTIDA
        fisica.AddForce(data.multiplicadorVelocidade * Velocity * Direction, ForceMode2D.Impulse);
        impulso.GenerateImpulse();
    }
    void FixedUpdate()
    {
        if(fisica.linearVelocity.magnitude > 0.5f)
        {
            // GIRAR BOLA DE FOGO PARA A DIREÇÃO QUE ESTÁ ANDANDO
            float ang = Mathf.Atan2(fisica.linearVelocity.y, fisica.linearVelocity.x)* Mathf.Rad2Deg;
            fisica.MoveRotation(ang);
        }
        else
        {
            //BOLA MORREU
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D cool)
    {
        Debug.Log("Bati");
        if(cool.gameObject.CompareTag("Ground"))
        {
            // TOCAS SOM
            AudioManager.instance.PlaySound("BolaBate");
            if(investida.estaInvestindo)
            {
                // ZERAR FORÇAS
                float velX = fisica.linearVelocityX;
                fisica.linearVelocity = Vector2.zero;
                // TROCAR VELOCIDADE Y POR X
                fisica.AddForce(new Vector2(Mathf.Abs(cool.relativeVelocity.y*0.4f), velX*0.7f), ForceMode2D.Impulse);
            }
            else
            {
                // REDUZIR VELOCIDADE X
                fisica.linearVelocityX *= 0.4f;
                // REZETAR VELOCIDADE Y E LANÇAR PARA CIMA COM VELOCIDADE MENOR
                fisica.linearVelocityY = 0;
                fisica.AddForce(Mathf.Abs(cool.relativeVelocity.y*0.7f) * Vector2.up, ForceMode2D.Impulse);
            }
            // PARAR INVESTIDA (e tambem dar efeito de Toing)
            investida.PararCiclo();
            // REDUZIR DANO
            ReciveDamage();
        }
    }

    public void ReciveDamage()
    {
        //TOCAR PARTICULA DE HIT
        if(particulaHIT.isEmitting)
        {
            particulaHIT.Stop();
        }
        particulaHIT.Play();

        // APLICAR TREMULAÇÃO NA CAMERA
        if (impulso != null) impulso.GenerateImpulse();
        // SE LENTO DEMAIS, DESTRUIR BOLA
        if(fisica.linearVelocityX <= 1f)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyModifiers(float speedMod, float heightMod, Vector2 velMinima)
    {
        // EFEITO DE DESACELERAÇÃO PERFORMÁTICO SE HOUVER GANHO DE VELOCIDADE
        if(speedMod >= 1 || heightMod >= 1)
        {
            tween?.Kill();
            tween = DOVirtual.Float(0.1f, 1f, 1f, (v) =>
            {
                Time.timeScale = v;
                Time.fixedDeltaTime = defaultFixedTime*v;
            }).SetUpdate(true).SetEase(Ease.OutSine);
        }
        // INFORMAR ERRO DE MAXIMOS E MINIMOS
        if(MaxVelocity == Vector2.zero)
        {
            Debug.LogError("Velocidade maxima não definida! (Verifique no prefab Bola de Fogo)");
        }
        // APLICAR GANHO DE VELOCIDADE X
        fisica.linearVelocityX = Mathf.Clamp(fisica.linearVelocityX*speedMod, velMinima.x, MaxVelocity.x);
        Debug.Log(fisica.linearVelocityX);

        // APLICAR GANHO DE VELOCIDADE Y
        if (heightMod != 0)
        {
            // PARAR INVESTIDA (e tambem dar efeito de Toing)
            investida.PararCiclo();
            // ZERAR VELOCIDADE Y
            float velY = Mathf.Abs(fisica.linearVelocityY);
            fisica.linearVelocityY = 0;
            // APLICAR IMPULSO PARA CIMA
            fisica.AddForce(new Vector2(0, velY*heightMod), ForceMode2D.Impulse);
            Debug.Log(velY*heightMod);
            fisica.linearVelocityY = Mathf.Clamp(fisica.linearVelocityY, velMinima.y, MaxVelocity.y);
        }
    }

    public void IncreasePoints(int index)
    {
        // AUMENTAR A QUANTIDADE DO OBSTACULO DE ÍNDICE 'INDEX'
        qntPerIndex[index]++;
    }

    void OnDestroy()
    {
        tween?.Kill();
        GameObject.Find("HUD").GetComponent<HUD_Points_Script>().IndexQnt = qntPerIndex;
        FimDePartida?.Invoke();
    }
}
