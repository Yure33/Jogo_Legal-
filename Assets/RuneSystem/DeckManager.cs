using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<RuneSO> activeDeck = new List<RuneSO>();
    [SerializeField] private List<RuneSO> runasCompradas = new List<RuneSO>();
    [SerializeField] private List<RuneSO> runasLoja = new List<RuneSO>();

    public List<RuneSO> GetDeck()
    {
        return activeDeck;
    }
}