    using System;
    using UnityEngine;

    public class Obstacle : MonoBehaviour
    {
    [SerializeField] ObstaculoNewSO data;
    [SerializeField] private BolaFogo_Script _ball;
    int _idData;
    public static event Action<int> OnGetPoints;

    void Start()
    {
        _idData = data.indexObstacle;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        _ball = collider.GetComponent<BolaFogo_Script>();
        if(_ball != null)
        {
            if (!string.IsNullOrEmpty(data.soundOnHit))
            {
                AudioManager.instance.PlaySound(data.soundOnHit);
            }

            _ball.ApplyModifiers(data.velocityModifier, data.heightModifier);
            _ball.IncreasePoints(_idData);
            _ball.ReciveDamage(0);

            if(data.destroyInTouch)
            {
                if (data.vfxDestruicaoPrefab != null)
                {
                    Instantiate(data.vfxDestruicaoPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }
}
