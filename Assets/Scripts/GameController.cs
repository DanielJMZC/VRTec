using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Transform ClientTarget;
    public Transform ClientDestination;

    void Awake()
    {
        Instance = this;
    }
}