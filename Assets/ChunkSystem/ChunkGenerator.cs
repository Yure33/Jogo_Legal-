using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Configurações dos Blocos")]
    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] private float chunkWidth = 20f;
    [SerializeField] private int maxChunksOnScreen = 3;

    [Header("Configurações de Posição")]
    [Tooltip("Posição X onde o primeiro bloco vai nascer (Zona Segura)")]
    [SerializeField] private float startX = 20f;
    
    [Tooltip("Altura mínima que o bloco pode nascer no eixo Y")]
    [SerializeField] private float minSpawnY = 3f;
    [Tooltip("Altura máxima que o bloco pode nascer no eixo Y")]
    [SerializeField] private float maxSpawnY = 3f; 

    private float nextSpawnX;
    private Transform camTransform;
    private Queue<GameObject> activeChunks = new Queue<GameObject>();

    void Start()
    {
        if (Camera.main != null) camTransform = Camera.main.transform;
        
        // Em vez de 0, a fita métrica começa no valor que você definiu
        nextSpawnX = startX;
        
        for (int i = 0; i < maxChunksOnScreen; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        if (camTransform.position.x > (nextSpawnX - (chunkWidth * 2)))
        {
            SpawnChunk();
            
            GameObject oldChunk = activeChunks.Dequeue();
            Destroy(oldChunk);
        }
    }

    private void SpawnChunk()
    {
        if (chunkPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, chunkPrefabs.Length);
        float randomY = Random.Range(minSpawnY, maxSpawnY);
        
        GameObject newChunk = Instantiate(chunkPrefabs[randomIndex], new Vector3(nextSpawnX, randomY, 0), Quaternion.identity);
        
        activeChunks.Enqueue(newChunk);
        nextSpawnX += chunkWidth;
    }
}