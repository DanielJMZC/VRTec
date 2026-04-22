using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class CleintController : MonoBehaviour
{
    private Transform target;
    private Transform offView;
    private Renderer targetRenderer;
    
    
    [SerializeField] private GameObject canvasDialogue; 
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] private float updateSpeed = 0.01f;
    [SerializeField] private float maxTemp;
    [SerializeField] private float minTemp;
    [SerializeField] private ClienteTimer clienteTimer;

    [SerializeField] private HamburguerStruct hamburguerStruct;
    public float waitTimeBeforeOffView=2f;
    List<string> CorrectOrderDialogue = new List<string>
    {
        "Yes! This is exactly what I wanted. You nailed it.",
        "Finally, someone who actually listens.",
        "Okay, I wasn't sure you'd get it, but this is perfect.",
        "This is exactly what I had in mind. Well done.",
        "Mmm. You understood the assignment.",
        "Don't tell anyone, but this might be the best burger I've ever had.",
        "You read my mind. Seriously.",
        "I'm coming back tomorrow. And the day after.",
        "That's it. That's the one. Thank you.",
        "Okay, you're forgiven for making me wait.",
    };
    List<string> IncorrectOrderDialogue = new List<string>
    {
        "This... this is not what I said.",
        "Did you even hear me? I'm not eating this.",
        "I feel like we had a miscommunication somewhere.",
        "I specifically said — you know what, never mind.",
        "Whoever made this clearly wasn't paying attention.",
        "I don't even know where to begin. This is wrong on multiple levels.",
        "I'm going to need you to try again.",
        "I appreciate the effort. I do. But no.",
        "There is definitely something in here that should not be in here.",
        "This is a disaster. A delicious-looking disaster, but still a disaster.",
    };
    private NavMeshAgent agent;
    private bool isRetiring = false;
    public List<string> order;
    public string nombreOrden, riddle;


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
            order = hamburguerStruct.GetOrder(out nombreOrden, out riddle);
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

                MostrarDialogo();
                StartCoroutine(WaitAndGoOffView());
            }
            if (isRetiring && target == offView && HasReachedDestination())
            {
                OcultarDialogo();
                
                Debug.Log("Cliente se ha retirado. Destruyendo objeto.");
                Destroy(gameObject);
                yield break;
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
            Destroy(other.gameObject);
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
        
        if (ListsMatch(order, platoList))
        {
            Debug.Log($"¡CORRECTO! El burger coincide con la orden '{nombreOrden}'");
            textDialogue.text = CorrectOrderDialogue[Random.Range(0, CorrectOrderDialogue.Count)];
            targetRenderer.material.color = Color.green;
            GameController.Instance.sFXManager.PlayClientHappySFX();
            GameController.Instance.UpdateScore(1);
            StartCoroutine(WaitAndGoOffView());
        }
        else
        {
            Debug.LogWarning($"¡INCORRECTO! El burger no coincide con la orden '{nombreOrden}'.");
            Debug.Log($"Se esperaba: {string.Join(", ", order)}");
            Debug.Log($"Se recibió: {string.Join(", ", platoList)}");
            textDialogue.text = IncorrectOrderDialogue[Random.Range(0, IncorrectOrderDialogue.Count)];
            targetRenderer.material.color = Color.red;
            GameController.Instance.sFXManager.PlayClientAngrySFX();
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
    public void MostrarDialogo()
    {
        if (canvasDialogue != null && textDialogue != null)
        {
            textDialogue.text = riddle; 
            canvasDialogue.SetActive(true); 
        }
    }

    public void OcultarDialogo()
    {
        if (canvasDialogue != null)
            canvasDialogue.SetActive(false);
    }
    
}