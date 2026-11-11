using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InvestidaScript : MonoBehaviour
{
    InputAction clique;

    [Header("Configurações da Investida")]
    [SerializeField] private float forca = 25f;
    private Rigidbody2D rb;
    private SpriteRenderer spritePrincipal;
    public bool estaInvestindo = false;
    public float delayPerUse;
    float timedDelay = 0;

    [SerializeField] private bool mouseOverUI = false;

    [Header("Configurações do Rastro (Ghost Trail)")]
    [SerializeField] private float intervaloFantasma = 0.05f; // Tempo entre cada sprite deixado
    [SerializeField] private float duracaoFadeFantasma = 0.3f; // Quanto tempo o rastro demora para sumir
    [SerializeField] private Color corFantasma = new Color(1f, 1f, 1f, 0.5f); // Cor e transparência inicial do rastro
    [SerializeField] GameObject circulo;
    private float cronometroFantasma;

    void Awake()
    {
        clique = InputSystem.actions.FindAction("Player/MouseClick");
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spritePrincipal = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        clique.performed += IniciarInvestida;
    }
    void OnDisable()
    {
        clique.performed -= IniciarInvestida;
    }

    void Update()
    {
        if (estaInvestindo)
        {
            cronometroFantasma += Time.deltaTime;
            if (cronometroFantasma >= intervaloFantasma)
            {
                GerarSpriteFantasma();
                cronometroFantasma = 0f;
            }
        }
        if(timedDelay >= 0 && !estaInvestindo)
        {
            timedDelay -= Time.deltaTime;
        }

        if (EventSystem.current != null)
        {
            mouseOverUI = EventSystem.current.IsPointerOverGameObject();
        }
    }

    void IniciarInvestida(InputAction.CallbackContext context)
    {
        if (mouseOverUI)
        {
            return;
        }

        if(!estaInvestindo && timedDelay < 0)
        {
            timedDelay = delayPerUse;
            Instantiate(circulo, transform.position, Quaternion.identity);
            ComecarCiclo();
        }
    }
    private void ComecarCiclo()
    {
        estaInvestindo = true;
        cronometroFantasma = 0f;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
        rb.AddForce(Vector2.down * forca, ForceMode2D.Impulse);
    }
    private void GerarSpriteFantasma()
    {
        GameObject fantasma = new GameObject("GhostSprite");
        fantasma.transform.position = transform.position;
        fantasma.transform.rotation = transform.rotation;
        fantasma.transform.localScale = transform.localScale;

        SpriteRenderer rendererFantasma = fantasma.AddComponent<SpriteRenderer>();
        rendererFantasma.sprite = spritePrincipal.sprite;
        rendererFantasma.sortingLayerID = spritePrincipal.sortingLayerID;
        rendererFantasma.sortingOrder = spritePrincipal.sortingOrder - 1;
        rendererFantasma.color = corFantasma;

        rendererFantasma.DOFade(0f, duracaoFadeFantasma)
            .SetEase(Ease.Linear)
            .OnComplete(() => Destroy(fantasma)); // Destrói o objeto para não poluir a cena
    }

    public void PararCiclo()
    {
        transform.DOComplete();
        transform.DOScale(transform.localScale * 0.8f, 0.1f).SetLoops(2, LoopType.Yoyo);
        estaInvestindo = false;
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}