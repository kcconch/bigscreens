using UnityEngine;

public class ClientData
{
    public string id;
    public Vector2 Input;
    private readonly View _view;

//    private readonly MeshDeformerInput _deform;
     

    public ClientData(string id)
    {
        Input = Vector2.zero;
        _view = View.Create(this);
       // Debug.Log("client connected");
//       _deform = MeshDeformerInput.Create(this);

    }

    public void Destroy()
    { 
        Object.Destroy(_view.gameObject);
        //  Object.Destroy(_deform.gameObject);
    }
}