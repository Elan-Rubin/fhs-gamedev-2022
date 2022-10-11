using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && Instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
