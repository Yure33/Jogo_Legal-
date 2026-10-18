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
}

[CreateAssetMenu(fileName = "NovoBancoDeObstaculos", menuName = "Configurações/Banco de Obstaculos")]
public class ObstacleDataSO : ScriptableObject {
    [Tooltip("Lista contendo as configurações de cada obstáculo")]
    [SerializeField] private ObstacleData[] _obstacleList;
    public ObstacleData[] obstacleList => _obstacleList;
}
