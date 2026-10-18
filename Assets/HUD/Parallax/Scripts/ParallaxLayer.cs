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
        float delta = cameraTransform.position.x * (1 - parallaxFactor);
        float distance = cameraTransform.position.x * parallaxFactor;
        transform.position = new Vector3(positionX + distance, transform.position.y, transform.position.z);

        if (delta > positionX + lenght)
        {
            positionX += lenght;
        }
        else if (delta < positionX - lenght)
        {
            positionX -= lenght;
        }
    }
}