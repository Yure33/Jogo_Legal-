using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    [Tooltip("Nome do tipo do objeto")]
    [SerializeField] private string _nameObstacle;
    public string NameObstacle => _nameObstacle;

    [Tooltip("Quantidae de pontos que irá ganhar")]
    [SerializeField] private int points;
    public int Points => points;

    [Tooltip("Index do obstaculo")]
    [SerializeField] private int indexObstacle;
    public int IndexObstacle => indexObstacle;

    [Header("Efeitos do obstaculo")]
    [SerializeField] private float velocityModifier;
    public float VelocityModifier => velocityModifier;

    [SerializeField] private float heightModifier;
    public float HeightModifier => heightModifier;

    [SerializeField] private bool destroyInTouch;
    public bool DestroyInTouch => destroyInTouch;

    [Header("Efeitos Fisicos")]
    [Header("Efeitos Físicos (Colisão Sólida - Parede, TNT)")]
    [SerializeField] private float damage = 1f;
    public float Damage => damage;

    [SerializeField] private float velMinToBreak = 10f;
    public float VelMinToBreak => velMinToBreak;

    [SerializeField] private float repulseForce = 15f;
    public float RepulseForce => repulseForce;

    [SerializeField] private float slowFactor = 0.6f;
    public float SlowFactor => slowFactor;

    [Header("Efeitos Visuais")]
    [SerializeField] private GameObject vfxDestruicaoPrefab;
    public GameObject VfxDestruicaoPrefab => vfxDestruicaoPrefab;

    [Header("Áudio")]
    [SerializeField] private string soundOnHit;
    public string SoundOnHit => soundOnHit;
}

[CreateAssetMenu(fileName = "NovoBancoDeObstaculos", menuName = "Configurações/Banco de Obstaculos")]
public class ObstacleDataSO : ScriptableObject {
    [Tooltip("Lista contendo as configurações de cada obstáculo")]
    [SerializeField] private ObstacleData[] _obstacleList;
    public ObstacleData[] ObstacleList => _obstacleList;
}
