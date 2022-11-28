using System.Collections.Generic;

public class Inventory : Component
{
    private List<Weapon> _weapons = new();

    private int _current;

    public Weapon Current
    {
        get => _weapons[_current];
    }

    private void Awake()
    {
        GetComponentsInChildren(_weapons);
    }

    private void Start()
    {
        _current = 0;
        for (int i = 1; i < _weapons.Count; i++)
        {
            _weapons[i].enabled = false;
        }
    }

    public void GetNext()
    {
        Current.OnExit();
        Current.enabled = false;
        if (_current == _weapons.Count - 1)
        {
            _current = 0;
        }
        else
        {
            _current++;
        }

        Current.enabled = true;
    }

    public void GetPrev()
    {
        Current.OnExit();
        Current.enabled = false;
        if (_current == 0)
        {
            _current = _weapons.Count - 1;
        }
        else
        {
            _current--;
        }

        Current.enabled = true;
    }
}
