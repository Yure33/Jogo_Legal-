using UnityEngine;
using UnityEngine.UI;
using TMPro;

//struct para as variaveis da UI. Cada icone da loja tbm vai ter o SO do item q esta sendo comprado
[System.Serializable]
public struct ItensLoja
{
    public Button botao;
    public Image Icon;
    public TextMeshProUGUI[] textos;
    public UpgradeSO dadosUpgrade;
}

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject painelShop;
    [SerializeField] ItensLoja[] itens;

    void Start() {
        foreach (ItensLoja item in itens) {
            item.botao.onClick.RemoveAllListeners();
    
            item.botao.onClick.AddListener(() => 
            {
                //"amarra" a chamada do evento ao clique do botao
                EventBusShop.DispararOnTryShop(item.dadosUpgrade);
            });

            TMP_Text[] textos = item.botao.GetComponentsInChildren<TMP_Text>();
            foreach(TMP_Text t in textos) {
                t.raycastTarget = false;
            }
        }
    }

    void OnEnable() {
        EventBusShop.OnShopActivate += AtivarUIShop;
        EventBusShop.OnUpgradeComprado += AtualizarShop;
    }

    void OnDisable() {
        EventBusShop.OnShopActivate -= AtivarUIShop;
        EventBusShop.OnUpgradeComprado -= AtualizarShop;
    }

    private void AtivarUIShop() {
        painelShop.SetActive(true);
        RedesenharVitrine();
    }

    private void AtualizarShop(UpgradeType tipo, float x) {
        if (painelShop.activeSelf) {
            RedesenharVitrine();
        }
    }

    private void RedesenharVitrine() {
        foreach (ItensLoja item in itens) {
            
            // carrega a imagem do SO no botao
            item.Icon.sprite = item.dadosUpgrade.iconeUpgrade;
            item.Icon.preserveAspect = true;

            // [0] O primeiro texto na hierarquia eh o Nome
            item.textos[0].text = item.dadosUpgrade.nome;

            // Consulta a memoria para saber o nivel atual
            int nivelAtual = UpgradeManager.GetNivel(item.dadosUpgrade.tipo);
                
            // [1] O segundo texto na hierarquia eh o Nivel (Ex: "Lvl 0/3")
            item.textos[1].text = $"Lvl {nivelAtual}/{item.dadosUpgrade.nivelMaximo}";
                
            // Verifica se chegou no limite máximo do upgrade
            if (nivelAtual >= item.dadosUpgrade.nivelMaximo) {
                // [2] O terceiro texto eh o Preco, que vira MAX
                item.textos[1].text = "Lvl. MAX";
                item.textos[2].text = $"Já dominado";
                
                // Trava o botão e aplica um tom cinza escuro
                item.botao.interactable = false;
                item.botao.image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            } 
            else {
                // [2] Puxa o custo correto e atualiza o texto do Preço
                int custoAtual = item.dadosUpgrade.valor[nivelAtual];
                item.textos[2].text = $"$ {custoAtual}";
                
                // Libera o botão e volta para a cor normal (branco não altera o sprite)
                item.botao.interactable = true;
                item.botao.image.color = Color.white;
            }
        }
    }
}
