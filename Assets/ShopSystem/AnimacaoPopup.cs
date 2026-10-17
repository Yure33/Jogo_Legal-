using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class AnimacaoPopup : MonoBehaviour
{
    public float tempoVida = 1f;    // Quanto tempo dura até sumir
    public float velocidadeSubida = 50f; // Velocidade que sobe na tela

    private TMP_Text texto;
    private Color corOriginal;
    private float tempoAtual = 0f;

    void Awake() {
        texto = GetComponent<TMP_Text>();
        corOriginal = texto.color;
    }

    void Update() {
        tempoAtual += Time.deltaTime;
        float progresso = tempoAtual / tempoVida;

        // 1. Sobe
        transform.position += Vector3.up * velocidadeSubida * Time.deltaTime;

        // 2. Faz o Fade Out (Some gradativamente)
        corOriginal.a = Mathf.Lerp(1f, 0f, progresso);
        texto.color = corOriginal;

        // 3. Se mata quando o tempo acaba
        if (tempoAtual >= tempoVida) {
            Destroy(gameObject);
        }
    }
}