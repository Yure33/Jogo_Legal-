using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private int ActualMoney;

    void Start() {
        if(PlayerPrefs.HasKey("Money"))
        {
            ActualMoney = PlayerPrefs.GetInt("Money");
            Debug.Log("Possuo salvo");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 300);
            ActualMoney = PlayerPrefs.GetInt("Money");
        }
        EventBusShop.DispararOnMoneyChanged(ActualMoney);
    }

    void OnEnable() {
        EventBusShop.OnAddMoney += AddMoney;
        EventBusShop.OnTryShop += ComprarUpgrade; 
    }

    void OnDisable() {
        EventBusShop.OnAddMoney -= AddMoney;
        EventBusShop.OnTryShop -= ComprarUpgrade; 
    }

    private void AddMoney(int valor) {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+valor);
        ActualMoney = PlayerPrefs.GetInt("Money");
        EventBusShop.DispararOnMoneyChanged(ActualMoney);
    }

    private void ComprarUpgrade(UpgradeSO upgrade) {
        int dinheiros = PlayerPrefs.GetInt("Money");
        UpgradeType tipo = upgrade.tipo;
        int nivel = UpgradeManager.GetNivel(tipo);

        if (nivel >= upgrade.nivelMaximo) return;
        
        int valor = upgrade.valor[nivel];

        if(valor <= dinheiros) {
            UpgradeManager.SetNivel(tipo, upgrade.nivelMaximo);
            EventBusShop.DispararOnUpgradeComprado(tipo, upgrade.valorEfeito[nivel]);
            dinheiros -= valor;
            PlayerPrefs.SetInt("Money", dinheiros);
            ActualMoney = PlayerPrefs.GetInt("Money");
            EventBusShop.DispararOnMoneyChanged(ActualMoney);
        }

        switch(upgrade.nome)
        {
            case "Velocity":
                upgrade.dataToApply.multiplicadorVelocidade = upgrade.valorEfeito[nivel];
                break;
            case "Size":
                upgrade.dataToApply.multiplicadorTamanho = upgrade.valorEfeito[nivel];
                break;
            case "Fortune":
                upgrade.dataToApply.dinheiroBonus = upgrade.valorEfeito[nivel];
                break;
        }
    }
}