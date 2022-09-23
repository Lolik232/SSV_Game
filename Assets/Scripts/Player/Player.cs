using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerState _idleState;

    private StateMachine _machine;

    private void Awake()
    {
        _machine = GetComponent<StateMachine>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
