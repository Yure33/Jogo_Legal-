using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TntFall : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SupportEntity _support;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        // Busca o balão dentro do GameObject Pai
        if (transform.parent != null)
        {
            _support = transform.parent.GetComponentInChildren<SupportEntity>();
        }

        // Se encontrou um balão, desliga a gravidade e "assina" o evento de morte dele
        if (_support != null)
        {
            _rb.gravityScale = 0f;
            _support.OnSupportDestroyed += Fall;
        }
    }

    private void Fall()
    {
        if (_rb != null)
        {
            _rb.gravityScale = 1f; // Liga a gravidade
        }
    }

    void OnDestroy()
    {
        //limpa a memoria
        if (_support != null)
        {
            _support.OnSupportDestroyed -= Fall;
        }
    }
}