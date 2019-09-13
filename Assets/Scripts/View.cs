using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class View : MonoBehaviour
{
    private static List<GameObject> _cars;
    private static CarPhysicsParams _params;

    private static int _carIndex = 0;

    private const float Scale = 10;
    
    public ClientData data;

    public static View Create(ClientData data)
    {
        if (_cars == null)
        {
            var carsList = Resources.Load<CarsList>("cars");
            if (carsList == null) throw new Exception("Could not find cars in Resources folder");
            _cars = carsList.Cars;
        }

        if (_params == null)
        {
            _params = Resources.Load<CarPhysicsParams>("carParams");
            if (_params == null) throw new Exception("Could not find cars in Resources folder");
        }

        var views = GameObject.Find("Views");
        if (views == null)
        {
            views = new GameObject("Views");
            views.transform.localScale = new Vector3(Scale, Scale, Scale);

        }
        
        var car = Instantiate(_cars[_carIndex++ % _cars.Count], views.transform, false);
        var rb = car.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;
        var view = car.AddComponent<View>();
        view.data = data;
        
        // ReSharper disable once PossibleNullReferenceException
        var pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, 0));
        pos.y = 0;
        view.transform.position = pos;

        return view;
    }
    
    private void Update()
    {
        var rb = GetComponent<Rigidbody>();
        
        var x = data.Input.x;
        var y = -data.Input.y;
        var inputVel = data.Speed * _params.SpeedScale * new Vector3(x, 0, y).normalized;

        rb.drag = _params.Drag;
        rb.angularDrag = _params.AngularDrag;
        
        // rb.velocity = inputVel;
        rb.AddForceAtPosition(inputVel, rb.position + transform.forward);
        transform.localScale = new Vector3(data.Scale, data.Scale, data.Scale);
        // transform.rotation = Quaternion.LookRotation(rb.velocity.normalized, transform.up);
    }
}