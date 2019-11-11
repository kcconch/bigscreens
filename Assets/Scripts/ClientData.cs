using UnityEngine;

public class ClientData
{
    public string Message;
    public float Scale;
    public float Speed;
    public Vector2 Input;
    private readonly View _view;

//    private readonly MeshDeformerInput _deform;
     

    public ClientData()
    {
        Message = "?";
        Scale = 1;
        Speed = 1;
        Input = Vector2.zero;
        _view = View.Create(this);
       // Debug.Log("client connected");
//       _deform = MeshDeformerInput.Create(this);

    }

    public void Destroy()
    {
        // Object.Destroy(_view.gameObject);
      //  Object.Destroy(_deform.gameObject);
    }
}