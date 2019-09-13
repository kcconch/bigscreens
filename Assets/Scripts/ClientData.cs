using UnityEngine;

public class ClientData
{
    public readonly int Number;
    public float Scale;
    public float Speed;
    public Vector2 Input;
    public View View;

    public ClientData(int number)
    {
        Number = number;
        Scale = 1;
        Speed = 1;
        Input = Vector2.zero;
        View = View.Create();
    }

    public void Destroy()
    {
        Object.Destroy(View.gameObject);
    }
}