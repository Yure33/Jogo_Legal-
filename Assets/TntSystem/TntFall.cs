using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TntFall : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private SupportEntity _support;
    [SerializeField] GameObject tntAnimacao;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Se encontrou um balão, desliga a gravidade e "assina" o evento de morte dele
        if (_support != null)
        {
            _rb.gravityScale = 0f;
        }
    }

    public void Fall()
    {
        if (_rb != null)
        {
            _rb.gravityScale = 1f; // Liga a gravidade
        }
        
        Destroy(gameObject, 2);
    }
}