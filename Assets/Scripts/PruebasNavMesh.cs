using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PruebasNavMesh : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform offView;
    [SerializeField] private float updateSpeed = 0.1f;
    [SerializeField] private float waitTimeBeforeOffView = 3.0f; // Tiempo de espera en segundos

    private NavMeshAgent agent;
    private bool isRetiring = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while (enabled)
        {
            // Actualizamos el destino
            agent.SetDestination(target.position);

            // Verificamos si ya llegó al destino (Target) y si no está ya en proceso de retirarse
            if (!isRetiring && target != offView && HasReachedDestination())
            {
                // Iniciamos la rutina de retirada
                StartCoroutine(WaitAndGoOffView());
            }

            yield return wait;
        }
    }

    private bool HasReachedDestination()
    {
        // Verificamos si el agente está cerca del destino y se ha detenido
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator WaitAndGoOffView()
    {
        isRetiring = true; // Bloqueamos para que no se ejecute varias veces
        
        // Esperamos el tiempo configurado
        yield return new WaitForSeconds(waitTimeBeforeOffView);
        
        // Cambiamos el objetivo al punto fuera de vista
        target = offView;
        
        // Opcional: Si quieres que una vez en OffView vuelva a buscar al Target original, 
        // tendrías que resetear 'isRetiring' y el 'target' en otro momento.
    }
}