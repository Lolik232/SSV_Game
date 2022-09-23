using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newUnitData", menuName = "Data/Unit Data/Base Data")]

public class UnitData : ScriptableObject
{
    [Header("Check Variables")]
    public float groundCheckRadius;
    public float groundIsCloseCheckDistance;
    public float wallCheckDistance;
    public LayerMask whatIsGround;

    [Header("Endurance")]
    public float baseEndurance = 10f;
    public float baseEnduranceRegeneration = 2f;

    [Header("Parameters")]
    public Vector2 size;
}
