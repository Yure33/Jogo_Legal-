using System;
using UnityEngine;

    public class Obstacle : MonoBehaviour
    {
    [SerializeField] ObstaculoNewSO data;
    [SerializeField] private BolaFogo_Script _ball;
    public static event Action<int> OnGetPoints;

    void OnTriggerEnter2D(Collider2D collider)
    {
        _ball = collider.GetComponent<BolaFogo_Script>();
        if(_ball != null)
        {
            // TOCAR SOM DE IMPACTO
            if (!string.IsNullOrEmpty(data.soundOnHit))
            {
                AudioManager.instance.PlaySound(data.soundOnHit);
            }
            if(data.velocidadeMinima_AoColidir == Vector2.zero) Debug.LogError("Ganho mínimo não definida no SO do Obstaculo colidido!");

            // APLICAR MODIFICADORES DE VELOCIDADE
            _ball.ApplyModifiers(data.velocityModifier, data.heightModifier, data.velocidadeMinima_AoColidir);
            // AUMENTAR PONTOS
            _ball.IncreasePoints(data.indexObstacle);
            // APLICAR EFEITO DE DANO E VERIFICAR VELOCIDADE ATUAL
            _ball.ReciveDamage();

            if(data.destroyInTouch)
            {
                // DESTUIR OBSTACULO
                if (data.vfxDestruicaoPrefab != null)
                {
                    Instantiate(data.vfxDestruicaoPrefab, transform.position, Quaternion.identity);
                }
                
                Destroy(gameObject);
            }   
            
            // AUMENTAR PONTOS AO VIVO
            OnGetPoints.Invoke(data.points);
        }
    }
}
