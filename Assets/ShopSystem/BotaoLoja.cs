using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BotaoLoja : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            EventBusShop.DispararOnShopActivate();

            //talvez fazer o jogo parar
        });
    }
}