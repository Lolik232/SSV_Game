using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.U2D.IK;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string _weaponName;
    [SerializeField] private GameObject _baseOrigin;

    protected SpriteRenderer Sr { get; private set; }
    protected Animator BaseAnim { get; private set; }
    protected Player Player { get; private set; }

    protected virtual void Awake()
    {
        Player = _baseOrigin.GetComponent<Player>();
        BaseAnim = _baseOrigin.GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {

    }

    public void OnEnter()
    {
        BaseAnim.SetBool(_weaponName, true);
        Sr.enabled = true;
    }

    public void OnExit()
    {
        BaseAnim.SetBool(_weaponName, false);
        Sr.enabled = false;
    }

    protected virtual void OnDrawGizmos()
    {

    }
}
