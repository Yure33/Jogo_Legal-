using UnityEngine;

[CreateAssetMenu(fileName = "NovoUpgrade", menuName = "Loja/Novo Upgrade")]
public class UpgradeSO : ScriptableObject {
    public string nome;
    public int[] valor;
    public UpgradeType tipo;
    public float[] valorEfeito; //vetor para o valor do efeito em cada nivel
    public Sprite iconeUpgrade;
    public BolaFogoSO dataToApply;
    public int nivelMaximo => valor.Length;
}