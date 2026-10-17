using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private int ActualMoney = 1000;

    void Start() {
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
        ActualMoney += valor;
        EventBusShop.DispararOnMoneyChanged(ActualMoney);
    }

    private void ComprarUpgrade(UpgradeSO upgrade) {
        int dinheiros = ActualMoney;
        UpgradeType tipo = upgrade.tipo;
        int nivel = UpgradeManager.GetNivel(tipo);

        if (nivel >= upgrade.nivelMaximo) return;
        
        int valor = upgrade.valor[nivel];

        if(valor <= dinheiros) {
            UpgradeManager.SetNivel(tipo, upgrade.nivelMaximo);
            EventBusShop.DispararOnUpgradeComprado(tipo, upgrade.valorEfeito[nivel]);
            dinheiros = dinheiros - valor;
            ActualMoney = dinheiros;
            EventBusShop.DispararOnMoneyChanged(ActualMoney);
        }
    }
}