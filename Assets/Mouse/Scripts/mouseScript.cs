using UnityEngine;
using UnityEngine.InputSystem;

public class mouseScript : MonoBehaviour
{
    Transform tf;
    [SerializeField] float Z_Value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if(mousePos != null)
        {
            tf.position = new Vector3(mousePos.x, mousePos.y, Z_Value);
        }
    }
}
