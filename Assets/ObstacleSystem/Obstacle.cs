using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  [SerializeField] private float _velMinToBreak = 10f;
  [SerializeField] private ObstacleDataSO dataObstacle;
  [SerializeField] private BolaFogo_Script _ball;
  [SerializeField] private float damage = 1f;

  [SerializeField] private float repulseForce = 15f;
  [SerializeField] private float slowFactor = 0.6f;

  public static event Action<int> OnGetPoints;

  [SerializeField] private int _idData;

  void OnCollisionEnter2D(Collision2D collision)
  {
    _ball = collision.gameObject.GetComponent<BolaFogo_Script>();
    if (_ball != null)
    {
      float forceImpact = collision.relativeVelocity.magnitude;
      _ball.ReciveDamage(damage);

      if (_ball.Vida <= 0)
      {
        Debug.Log("A bola perdeu toda a vida e foi destruida");
        _ball.DestroyBall();
        return;
      }

      // quebrar obstaculo
      if (forceImpact >= _velMinToBreak)
      {
        ObstacleData myData = dataObstacle.obstacleList[_idData];
        Debug.Log($"Quebrou {myData.NameObstacle} e ganhou {myData.Points} pontos");

        // enviar os pontos ganhos para o Gerenciador de jogo
        OnGetPoints?.Invoke(myData.Points);

        _ball.SlowDown(slowFactor);
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
          _ball.Repulse(normal, repulseForce);
        }
      }
    }
  }
 
}
