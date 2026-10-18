using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.left; 
    [SerializeField] private float speed = 3f;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
