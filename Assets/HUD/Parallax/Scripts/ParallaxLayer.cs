using UnityEngine;
using UnityEngine.UI;

public class RawImageParallax : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Range(0,1f)] private float parallaxFactor;

    private float positionX;
    private float lenght;

    // Event function
    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        positionX = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log(lenght);
    }

    // Update is called once per frame
    // Event function
    void LateUpdate()
    {
        float camX = cameraTransform.position.x;
        float delta = camX * (1 - parallaxFactor);
        float distance = camX * parallaxFactor;
        transform.position = new Vector3(positionX + distance, transform.position.y, transform.position.z);

        // margem baseada no zoom atual
        float halfVisible = Camera.main.orthographicSize * Camera.main.aspect;

        // while, para não falhar em movimentos rápidos
        while (delta > positionX + lenght - halfVisible)
            positionX += lenght;
        while (delta < positionX - lenght + halfVisible)
            positionX -= lenght;
    }
}