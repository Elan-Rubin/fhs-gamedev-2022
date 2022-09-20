using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the Game Manager (as the name suggests)
    // It is a special C# file in unity that, well, manages the game
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
