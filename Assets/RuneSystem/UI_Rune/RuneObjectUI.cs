using UnityEngine.UI;
using UnityEngine;

//classe para configurar o objeto da runa na UI(imagem, nome, e SO)
public class RuneObjectUI : MonoBehaviour
{
    private Image runeImage;
    private RuneSO runeData;

    void Awake()
    {
        runeImage = GetComponent<Image>();
    }

    //configura runa
    public void ConfigRune(RuneSO data)
    {
        runeData = data;
        
        if(runeImage != null) runeImage.sprite = runeData.RuneIcon;

        gameObject.name = runeData.RuneName;
    }

    //executa efeito
    public void ApplyEffect()
    {
        GameObject rune = runeData.EfeitoPrefab;
    }
}