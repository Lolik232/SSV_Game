using System;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private Single _timeActivated;

    private Single _alpha;

    private Transform _player;

    private SpriteRenderer _sr;
    private SpriteRenderer _playerSr;

    private Color _color;

    private void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSr = _player.GetComponent<SpriteRenderer>();
    }
}
