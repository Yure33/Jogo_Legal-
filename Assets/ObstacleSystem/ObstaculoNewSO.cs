using UnityEngine;

[CreateAssetMenu(fileName = "ObstaculoData", menuName = "Configurações/Banco de Obstaculos", order = 0)]
public class ObstaculoNewSO : ScriptableObject
{
    [Tooltip("Nome do tipo do objeto")]
    public string nome;
    [Tooltip("Quantidae de pontos que irá ganhar")]
    public int points;
    [Tooltip("Index do obstaculo")]
    public int indexObstacle;

    [Header("Efeitos do obstaculo")]
    public float velocityModifier;

    public float heightModifier;

    public bool destroyInTouch;

    [Header("Efeitos Fisicos")]
    [Header("Efeitos Físicos (Colisão Sólida - Parede, TNT)")]
    public float damage;

    public float velMinToBreak;

    public float repulseForce;

    public float slowFactor;

    [Header("Efeitos Visuais")]
    public GameObject vfxDestruicaoPrefab;

    [Header("Áudio")]
    public string soundOnHit;
}
