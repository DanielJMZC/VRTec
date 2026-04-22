using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;


[RequireComponent(typeof(NavMeshAgent))]
public class CleintController : MonoBehaviour
{
    static public CleintController Instance;
    [SerializeField] private Transform target;
    [SerializeField] private Transform offView;
    [SerializeField] private float updateSpeed = 0.01f;
    [SerializeField] private float maxTemp;
    [SerializeField] private float minTemp;
    [SerializeField] private ClienteTimer clienteTimer;
    public float waitTimeBeforeOffView=50f;

    private NavMeshAgent agent;
    private bool isRetiring = false;
    public List<string> order;
    public string nombreOrden;


    void Awake()
    {
        Instance = this;
        agent = GetComponent<NavMeshAgent>();
        waitTimeBeforeOffView= Random.Range(minTemp, maxTemp);
    }

    void Start()
    {
        StartCoroutine(FollowTarget());
        if (clienteTimer != null)
        {
            clienteTimer.StartTimer();
        }
        else
        {
            Debug.LogWarning("ClienteTimer no está asignado en CleintController");
        }

        if(HamburguerStruct.Instance != null)
        {
            Debug.Log("HamburguerStruct.Instance está inicializado");
            order = HamburguerStruct.Instance.GetOrder(out nombreOrden);
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
