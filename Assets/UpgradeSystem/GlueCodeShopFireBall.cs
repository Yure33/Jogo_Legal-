using System;
using UnityEngine;

public class GlueCodeShopFireBall : MonoBehaviour
{
    public BolaFogoSO dadosBF;

    void Awake()
    {
        if(dadosBF != null)
        {
            dadosBF.ResetarInicial();
        }
    }

    void OnEnable()
    {
        EventBusShop.OnUpgradeComprado += AtualizarSO;
    }

    void OnDisable()
    {
        EventBusShop.OnUpgradeComprado -= AtualizarSO;
    }

    void AtualizarSO(UpgradeType tipo, float valorEfeito)
    {
        switch(tipo)
        {
            case UpgradeType.Velocity:
                dadosBF.multiplicadorVelocidade = valorEfeito;
                break;

            case UpgradeType.Size:
                dadosBF.multiplicadorTamanho = (int)valorEfeito;
                break;
            
            case UpgradeType.Fortune:
                dadosBF.dinheiroBonus = valorEfeito;
                break;

            default: 
                break;
        }
    }
}