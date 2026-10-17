using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  [SerializeField] private float _velMinToBreak = 10f;
  [SerializeField] private ObstacleDataSO dataObstacle;

  private int _idData;

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.relativeVelocity.magnitude >= _velMinToBreak)
    {
      ObstacleData myData = dataObstacle.obstacleList[_idData];

      Debug.Log($"Quebrou um obstáculo do tipo {myData.NameObstacle} e ganhou {myData.Points} pontos");
        Debug.Log($"Index do obstáculo: {myData.IndexObstacle}");

      Destroy(gameObject);
    }
  }
}
