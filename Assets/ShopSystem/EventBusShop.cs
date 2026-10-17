using UnityEngine;
using System;

public static class EventBusShop {
    //evento ativar UI loja
    public static event Action OnShopActivate;

    //eventos ativado quando o botao eh apertado
    public static event Action<UpgradeSO> OnTryShop;

    //evento para quando pegar mais dinheiro
    public static event Action<int> OnAddMoney;

    //evento lancado quando a compra eh efetivada, para atualizar a UI e avisar a bola de fogo
    public static event Action<UpgradeType, float> OnUpgradeComprado;

    public static event Action<int> OnMoneyChanged;
    
    public static void DispararOnShopActivate() 
    {
        OnShopActivate?.Invoke();
    }

    public static void DispararOnTryShop(UpgradeSO upgrade) 
    {
        OnTryShop?.Invoke(upgrade);
    }

    public static void DispararOnAddMoney(int valor) 
    {
        OnAddMoney?.Invoke(valor);
    }

    public static void DispararOnUpgradeComprado(UpgradeType tipo, float valor) 
    {
        OnUpgradeComprado?.Invoke(tipo, valor);
    }

    public static void DispararOnMoneyChanged(int saldo) 
    {
        OnMoneyChanged?.Invoke(saldo);
    }
}