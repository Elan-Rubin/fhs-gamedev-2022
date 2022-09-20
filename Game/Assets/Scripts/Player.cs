using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player scripts")]
    [Tooltip("The player's movement script.")]
    [SerializeField] private PlayerMovement _playerMovement;
    [Space(5)]

    private static Player _instance;
    public static Player Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    void Start()
    {
        if (_playerMovement == null) _playerMovement = GetComponent<PlayerMovement>();
        Debug.Log("hi!");
    }

    void Update()
    {
        
    }
}
