using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Transform ClientTarget;
    public Transform ClientDestination;

     public Behaviour moveProvider; // Continuous Move Provider
    public Behaviour teleportProvider;

    void Awake()
    {
        Instance = this;
    }

       void Start()
    {
        moveProvider.enabled = false;
        teleportProvider.enabled = false;
    }

    public void EnableMovement()
    {
        moveProvider.enabled = true;
        teleportProvider.enabled = true;
    }
}