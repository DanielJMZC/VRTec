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
    [SerializeField] private string codigoTarget = "FollowPoint";
    [SerializeField] private string codigoOffView = "ExitPoint";
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

        agent = GetComponent<NavMeshAgent>();
        waitTimeBeforeOffView= Random.Range(minTemp, maxTemp);

        if (target == null && !string.IsNullOrEmpty(codigoTarget))
        {
            target = BuscarTransformPorCodigo(codigoTarget);
            if (target == null)
                Debug.LogWarning("No se encontró target con código: " + codigoTarget);
        }
        
        if (offView == null && !string.IsNullOrEmpty(codigoOffView))
        {
            offView = BuscarTransformPorCodigo(codigoOffView);
            if (offView == null)
                Debug.LogWarning("No se encontró offView con código: " + codigoOffView);
        }
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
            if (target != null) // ADD THIS CHECK!
            {
                    agent.SetDestination(target.position);
            }
            else{
                Debug.LogWarning("Target is null!");
            }
            
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
    
    private Transform BuscarTransformPorCodigo(string codigo)
    {
        // Busca por nombre de objeto
        GameObject foundObject = GameObject.Find(codigo);
        if (foundObject != null)
            return foundObject.transform;
        
        // Busca por tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(codigo);
        if (taggedObjects.Length > 0)
            return taggedObjects[0].transform;
        
        // Busca en hijos recursivamente
        Transform found = BuscarEnHijos(transform.parent, codigo);
        if (found != null)
            return found;
        
        return null;
    }
    
    private Transform BuscarEnHijos(Transform padre, string codigo)
    {
        if (padre == null)
            return null;
        
        foreach (Transform hijo in padre.GetComponentsInChildren<Transform>())
        {
            if (hijo.gameObject.name == codigo)
                return hijo;
        }
        return null;
    }
}
