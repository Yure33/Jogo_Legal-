using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  [SerializeField] private float _velMinToBreak = 10f;
  [SerializeField] private ObstacleDataSO dataObstacle;
  [SerializeField] private TestePlayer _ball;
  [SerializeField] private float damage = 1f;

  [SerializeField] private float respulseForce = 15f;
  [SerializeField] private float slowFactor = 0.6f;


  private int _idData;

  void OnCollisionEnter2D(Collision2D collision)
  {
      _ball = collision.gameObject.GetComponent<TestePlayer>();

      if (_ball != null)
    {
      float forceImpact = collision.relativeVelocity.magnitude;
      _ball.Resistency -= damage;

      if (_ball.Resistency <= 0)
      {
        Debug.Log("A bola perdeu todo HP e foi destruida");
        _ball.Destroy();
        return;
      }

      if (forceImpact >= _velMinToBreak)
      {
        ObstacleData myData = dataObstacle.obstacleList[_idData];
        Debug.Log($"Quebrou {myData.NameObstacle} e ganhou {myData.Points} pontos");

        _ball.SlowDown(slowFactor);
        
        Destroy(gameObject);
      }
      else
      {
          if (forceImpact <= 2f)
        {
          Debug.Log("Bola bateu muito fraco e ficou parada");
          _ball.Stop();
        }else
        {
          Debug.Log("A bola bateu, não conseguiu quebrar e foi repelida!");
                    
                    // contacts[0].normal pega a inclinação exata da face da parede que a bola encostou
                    Vector2 normal = collision.contacts[0].normal;
                    _ball.Repulse(normal, respulseForce);
        }
      }
    }
  }
}
