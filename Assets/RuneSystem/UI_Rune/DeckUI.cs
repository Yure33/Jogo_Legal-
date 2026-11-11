using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private DeckManager deck;
    [SerializeField] private GameObject prefabRuna; //item(objeto que contem o draggable item)
    [SerializeField] private Transform hud_runas;

    private BolaFogo_Script bolaDeFogo;

    void Awake()
    {
        bolaDeFogo = Object.FindAnyObjectByType<BolaFogo_Script>();
        
        if (bolaDeFogo == null)
        {
            Debug.LogError("DeckUI não encontrou a Bola de Fogo na cena!");
        }
    }

    void OnEnable()
    {
        GerarRunas();
    }

    private void GerarRunas()
    {
        foreach(Transform slot in hud_runas)
        {
            if(slot.childCount > 0)
                Destroy(slot.GetChild(0).gameObject); //destroi o item(runa) em cada slot
        }

        List<RuneSO> runas = deck.GetDeck();

        //vai em cada filho do grid(slots) e gera  acarta correspondente
        for(int i = 0; i < runas.Count; i++)
        {
            if(i >= hud_runas.childCount)
            {
                Debug.LogWarning("Mais runas do que slots");
                break;
            }

            Transform slotDispo = hud_runas.GetChild(i);

            GameObject novaRuna = Instantiate(prefabRuna, slotDispo);

            RuneObjectUI runa = novaRuna.GetComponent<RuneObjectUI>();

            runa.ConfigRune(runas[i], bolaDeFogo);
        }
    }
}