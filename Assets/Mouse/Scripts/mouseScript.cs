using UnityEngine;
using UnityEngine.InputSystem;

public class mouseScript : MonoBehaviour
{
    Transform tf;
    [SerializeField] float Z_Value;
    public bool IsPressing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // CLIQUEI NO BOTAO DO MOUSE/LANÇAMENTO
            IsPressing = true;
        }
        else if(context.canceled)
        {
            // SOLTEI O BOTÃO DO MOUSE/LANÇÇAMENTO
            IsPressing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if(mousePos != null)
        {
            tf.position = new Vector3(mousePos.x, mousePos.y, Z_Value);
        }
    }
}
