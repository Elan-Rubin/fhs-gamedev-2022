using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player scripts")]
    [Tooltip("The player's movement script.")]
    [SerializeField] private PlayerMovement playerMovement;
    [Space(5)]

    private static Player instance;
    public static Player Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && Instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Start()
    {
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

    }
}
