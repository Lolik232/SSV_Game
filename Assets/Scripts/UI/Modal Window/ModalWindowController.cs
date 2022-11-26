using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalWindowController : MonoBehaviour
{
    public static ModalWindowController instance;

    [SerializeField] private ModalWindowPanel _modalWindow;

    public ModalWindowPanel ModalWindow => _modalWindow;

    private void Awake()
    {
        instance = this;
    }
}
