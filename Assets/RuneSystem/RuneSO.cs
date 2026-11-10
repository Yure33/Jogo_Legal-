using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "RuneData", menuName = "Runas", order = 1)]
public class RuneSO : ScriptableObject
{
    [SerializeField] private string runeName;
    [SerializeField] private Sprite runeIcon;
    [SerializeReference] private GameObject efeitoPrefab;

    public string RuneName => runeName;
    public Sprite RuneIcon => runeIcon;
    public GameObject EfeitoPrefab => efeitoPrefab;
}