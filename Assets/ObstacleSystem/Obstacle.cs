    using System;
    using UnityEngine;

    public class Obstacle : MonoBehaviour
    {
    [SerializeField] private ObstacleDataSO dataObstacle;
    [SerializeField] private BolaFogo_Script _ball;
    [SerializeField] private int _idData;

    public static event Action<int> OnGetPoints;
    void OnTriggerEnter2D(Collider2D collider)
    {
        _ball = collider.GetComponent<BolaFogo_Script>();
        if(_ball != null)
        {
            ObstacleData myData = dataObstacle.ObstacleList[_idData];

            if (!string.IsNullOrEmpty(myData.SoundOnHit))
            {
                AudioManager.instance.PlaySound(myData.SoundOnHit);
            }

            _ball.ApplyModifiers(myData.VelocityModifier, myData.HeightModifier);

            _ball.ReciveDamage(0);

            if(myData.Points > 0) OnGetPoints?.Invoke(myData.Points);

            if(myData.DestroyInTouch)
            {
                if (myData.VfxDestruicaoPrefab != null)
                {
                    Instantiate(myData.VfxDestruicaoPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _ball = collision.gameObject.GetComponent<BolaFogo_Script>();
        if (_ball != null)
        {
            ObstacleData myData = dataObstacle.ObstacleList[_idData];

            if (!string.IsNullOrEmpty(myData.SoundOnHit))
            {
                AudioManager.instance.PlaySound(myData.SoundOnHit);
            }
            
            float forceImpact = collision.relativeVelocity.magnitude;
            _ball.ReciveDamage(myData.Damage);

            if (_ball.Vida <= 0)
            {
                Debug.Log("A bola perdeu toda a vida e foi destruida");
                _ball.DestroyBall();
                return;
            }

            // quebrar obstaculo
            if (forceImpact >= myData.VelMinToBreak)
            {
                Debug.Log($"Quebrou {myData.NameObstacle} e ganhou {myData.Points} pontos");

                // enviar os pontos ganhos para o Gerenciador de jogo
                OnGetPoints?.Invoke(myData.Points);

                _ball.SlowDown(myData.SlowFactor);
                Destroy(gameObject);
            }
            else // logica se nao quebrar (impacto baixo)
            {
                if (forceImpact <= 2f)
                {
                Debug.Log("Bola bateu muito fraco, a bola ficou parada");
                _ball.Stop();
                }
                else
                {
                Debug.Log("A bola bateu, nao conseguiu quebrar o obstaculo, mas foi repulsada");
                Vector2 normal = collision.contacts[0].normal;
                _ball.Repulse(normal, myData.RepulseForce);
                }
            }
        }
    }
}
