using UnityEngine;

[CreateAssetMenu(fileName = "BolaDeFogo", menuName = "BolaFogo/Caracteristicas")]
public class BolaFogoSO: ScriptableObject
{
    public float multiplicadorVelocidade = 1f;
    public float multiplicadorTamanho = 1;
    public float dinheiroBonus = 1f;

    public void OnDisable()
    {
        multiplicadorVelocidade = 1f;
        multiplicadorTamanho = 1;
        dinheiroBonus = 1f;
    }
}