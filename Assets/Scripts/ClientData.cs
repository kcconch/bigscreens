using UnityEngine;

public class ClientData
{
    public readonly int Number;
    public float Scale;
    public float Speed;
    public Vector2 Input;
    private readonly View _view;

    public ClientData(int number)
    {
        Number = number;
        Scale = 1;
        Speed = 1;
        Input = Vector2.zero;
        _view = View.Create(this);
    }

    public void Destroy()
    {
        Object.Destroy(_view.gameObject);
    }
}