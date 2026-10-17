using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text prefabTextoFlutuante;     
    [SerializeField] private Transform pontoDeNascimentoPopup; 

    [SerializeField] float velocidadeDaRolagem = 150f; // Velocidade

    private TMP_Text textoDinheiro;
    private int saldoAlvo;
    private float saldoExibido;

    private void Awake() {
        textoDinheiro = GetComponent<TMP_Text>();
    }

    private void OnEnable() {
        EventBusShop.OnMoneyChanged += ReceberNovoSaldo;
    }

    private void OnDisable() {
        EventBusShop.OnMoneyChanged -= ReceberNovoSaldo;
    }

    // O EventBus chama essa função
    private void ReceberNovoSaldo(int novoSaldo) {
        int diferenca = novoSaldo - saldoAlvo; // Calcula o quanto mudou
        saldoAlvo = novoSaldo; // Define o novo alvo

        if (diferenca != 0) {
            GerarPopup(diferenca);
        }
    }

    private void Update() {
        // Se o numero na tela for diferente do saldo real
        if (Mathf.RoundToInt(saldoExibido) != saldoAlvo) {
            
            // MoveTowards vai rolando o numero em velocidade constante
            saldoExibido = Mathf.MoveTowards(saldoExibido, saldoAlvo, velocidadeDaRolagem * Time.deltaTime);
            
            textoDinheiro.text = $"$ {Mathf.RoundToInt(saldoExibido)}";
        }
    }

    private void GerarPopup(int diferenca) {
        if (prefabTextoFlutuante == null || pontoDeNascimentoPopup == null) return;

        TMP_Text popup = Instantiate(prefabTextoFlutuante, pontoDeNascimentoPopup);
        
        popup.rectTransform.localPosition = Vector3.zero;
        
        if (diferenca > 0) {
            popup.text = $"+{diferenca}";
            popup.color = Color.green;
        } else {
            popup.text = $"{diferenca}";
            popup.color = Color.red;
        }
    }
}