using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;


[RequireComponent(typeof(NavMeshAgent))]
public class CleintController : MonoBehaviour
{
    private Transform target;
    private Transform offView;
    [SerializeField] private float updateSpeed = 0.01f;
    [SerializeField] private float maxTemp;
    [SerializeField] private float minTemp;
    [SerializeField] private ClienteTimer clienteTimer;

    [SerializeField] private HamburguerStruct hamburguerStruct;
    public float waitTimeBeforeOffView=50f;

    private NavMeshAgent agent;
    private bool isRetiring = false;
    public List<string> order;
    public string nombreOrden;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        waitTimeBeforeOffView= Random.Range(minTemp, maxTemp);

        clienteTimer = GetComponent<ClienteTimer>();
        hamburguerStruct = FindFirstObjectByType<HamburguerStruct>();
    }

    void Start()
    {
        target = GameController.Instance.ClientTarget;
        offView = GameController.Instance.ClientDestination;

        StartCoroutine(FollowTarget());
        if (clienteTimer != null)
        {
            clienteTimer.StartTimer();
        }
        else
        {
            Debug.LogWarning("ClienteTimer no está asignado en CleintController");
        }

        if(hamburguerStruct != null)
        {
            order = hamburguerStruct.GetOrder(out nombreOrden);
            Debug.Log("Orden generada: " + nombreOrden);
        }else
        {
            Debug.LogWarning("HamburguerStruct.Instance no está inicializado");
        }
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while (enabled)
        {
            
            agent.SetDestination(target.position);

            
            if (!isRetiring && target != offView && HasReachedDestination())
            {
                
                StartCoroutine(WaitAndGoOffView());
            }

            yield return wait;
        }
    }

    private bool HasReachedDestination()
    {
        
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
        isRetiring = true;
        yield return new WaitForSeconds(waitTimeBeforeOffView);
        
    
        target = offView;
    }
}
