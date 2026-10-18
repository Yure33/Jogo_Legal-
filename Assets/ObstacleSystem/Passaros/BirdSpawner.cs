using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [Tooltip("Arraste o Prefab do Pássaro para cá")]
    [SerializeField] private GameObject birdPrefab;
    
    [Tooltip("Tempo em segundos entre cada pássaro")]
    [SerializeField] private float spawnInterval = 2f;
    
    [Header("Limites de Altura (Eixo Y)")]
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 4f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            SpawnBird();
        }
    }

    private void SpawnBird()
    {
        if (birdPrefab != null)
        {
            float randomY = Random.Range(minY, maxY);
            
            Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);

            Instantiate(birdPrefab, spawnPos, Quaternion.identity);
        }
    }
}