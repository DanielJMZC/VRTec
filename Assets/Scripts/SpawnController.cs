using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] public float minSpawnDelay;
    [SerializeField] public float maxSpawnDelay;

    private bool canSpawn = false;

    public void StartSpawning()
    {
        Debug.Log("StartSpawning llamado");
        Debug.Log("clientPrefab: " + (clientPrefab != null ? clientPrefab.name : "NULL"));
        Debug.Log("spawnPoint: " + (spawnPoint != null ? spawnPoint.name : "NULL"));
        Debug.Log("minSpawnDelay: " + minSpawnDelay);
        Debug.Log("maxSpawnDelay: " + maxSpawnDelay);
        
        if (clientPrefab == null || spawnPoint == null)
        {
            Debug.LogError("ERROR: clientPrefab o spawnPoint no están asignados en SpawnController");
            return;
        }
        
        canSpawn = true;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        Debug.Log("SpawnRoutine INICIADA - canSpawn: " + canSpawn); 
        while (canSpawn)
        {
            float nextWait = Random.Range(minSpawnDelay, maxSpawnDelay);
            Debug.Log("Esperando " + nextWait + " segundos antes de spawnear");
            yield return new WaitForSeconds(nextWait);
            
            Debug.Log("Intentando spawnear cliente...");
            SpawnClient();
        }
        Debug.Log("SpawnRoutine FINALIZADA");
    }

    private void SpawnClient()
    {
        if(clientPrefab!= null && spawnPoint != null)
        {
            Debug.Log("Instanciando prefab: " + clientPrefab.name + " en punto: " + spawnPoint.position);
            GameObject newClient=Instantiate(clientPrefab, spawnPoint.position, spawnPoint.rotation);

            newClient.SetActive(true); // Asegura que el objeto esté activo
            newClient.name = "Cliente_" + Time.time;
            newClient.hideFlags = HideFlags.None;
            Debug.Log("Objeto instanciado: " + newClient.name + " en posición: " + newClient.transform.position);
        }
        else
        {
            Debug.Log("Error en spawn: clientPrefab=" + clientPrefab + ", spawnPoint=" + spawnPoint);
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}