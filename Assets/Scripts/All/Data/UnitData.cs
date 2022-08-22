using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newUnitData", menuName = "Data/Unit Data/Base Data")]

public class UnitData : ScriptableObject
{
    [Header("Check Variables")]
    public Single groundCheckRadius;
    public Single groundIsCloseCheckDistance;
    public Single wallCheckDistance;
    public LayerMask whatIsGround;
}