using UnityEngine.UI;
using UnityEngine;

//classe para configurar o objeto da runa na UI(imagem, nome, e SO)
public class RuneObjectUI : MonoBehaviour
{
    private Image runeImage;
    private RuneSO runeData;
    private BolaFogo_Script bolaDeFogo;

    void Awake()
    {
        runeImage = GetComponent<Image>();
    }

    //configura runa
    public void ConfigRune(RuneSO data, BolaFogo_Script fireBall)
    {
        runeData = data;
        bolaDeFogo = fireBall;

        if(runeImage != null) runeImage.sprite = runeData.RuneIcon;

        gameObject.name = runeData.RuneName;
    }

    //executa efeito
    public void ApplyEffect()
    {
        GameObject runePrefab = runeData.EfeitoPrefab;

        IRuneEffect rune = runePrefab.GetComponent<IRuneEffect>();

        rune.ExecuteEffect(bolaDeFogo);
    }
}