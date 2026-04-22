using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    private bool canSpawn= true;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        Debug.Log("Iniciando rutina de spawn"); 
        while (canSpawn)
        {
            float nextWait=Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(nextWait);

            SpawnClient();
        }
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
