using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class CleintController : MonoBehaviour
{
    private Transform target;
    private Transform offView;
    private Renderer targetRenderer;
    
    [SerializeField] private float updateSpeed = 0.01f;
    [SerializeField] private float maxTemp;
    [SerializeField] private float minTemp;
    [SerializeField] private ClienteTimer clienteTimer;

    [SerializeField] private HamburguerStruct hamburguerStruct;
    public float waitTimeBeforeOffView=1f;

    private NavMeshAgent agent;
    private bool isRetiring = false;
    public List<string> order;
    public string nombreOrden;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        waitTimeBeforeOffView= Random.Range(minTemp, maxTemp);

        Transform hijo = transform.Find("Head");

        clienteTimer = GetComponent<ClienteTimer>();
        hamburguerStruct = FindFirstObjectByType<HamburguerStruct>();
        targetRenderer = hijo.GetComponent<Renderer>();
        
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plato"))
        {
            Debug.Log("Cliente ha recibido un plato");
            CompararPlatos(other.gameObject);
        }
    }

    private void CompararPlatos(GameObject plato)
    {
        BurgerStack burgerStack = plato.GetComponent<BurgerStack>();
        
        if (burgerStack == null)
        {
            Debug.LogError("El plato no tiene el componente BurgerStack");
            return;
        }
        
        List<string> platoList = burgerStack.ingredientsInBurger;
        
        if (platoList == null || platoList.Count == 0)
        {
            Debug.LogError("El burger no tiene ingredientes");
            return;
        }
        
        if (ListsMatch(order, platoList))
        {
            Debug.Log($"¡CORRECTO! El burger coincide con la orden '{nombreOrden}'");
            StartCoroutine(WaitAndGoOffView());
        }
        else
        {
            Debug.LogWarning($"¡INCORRECTO! El burger no coincide con la orden '{nombreOrden}'.");
            Debug.Log($"Se esperaba: {string.Join(", ", order)}");
            Debug.Log($"Se recibió: {string.Join(", ", platoList)}");
            targetRenderer.material.color = Color.red;
            StartExit(0.5f);
        }
    }
    
    private void StartExit(float delay)
    {
        isRetiring = true;
        if (delay > 0)
            StartCoroutine(WaitAndGo(delay));
        else
            target = offView;
    }
    
    private IEnumerator WaitAndGo(float delay)
    {
        yield return new WaitForSeconds(delay);
        target = offView;
    }
    
    private bool ListsMatch(List<string> lista1, List<string> lista2)
    {
        if (lista1 == null || lista2 == null)
            return false;
        
        if (lista1.Count != lista2.Count)
            return false;
        
        

        for (int i = 0; i < lista1.Count; i++)
        {
            if (lista1[i] != lista2[i])
                return false;
        }
        
        return true;
    }

    
}