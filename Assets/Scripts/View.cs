using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class View : MonoBehaviour
{
    private static List<GameObject> _cars;
    private static CarPhysicsParams _params;
    private static int _carIndex;
    private static Camera _cam;
    
    
    public ClientData data;
    public Canvas uiCanvas;
    public Text text;
    
    
    public static View Create(ClientData data)
    {

        if (_cam == null)
        {
            _cam = Camera.main;
            if (_cam == null) throw new Exception("Views need a main camera in the scene to orient themselves.");
        }
        
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

        const float Scale = 7;
        
        var views = GameObject.Find("Views");
        if (views == null)
        {
            views = new GameObject("Views");
            views.transform.localScale = new Vector3(Scale, Scale, Scale);

        }
        
        var car = Instantiate(_cars[_carIndex++ % _cars.Count], views.transform, false);
        var rb = car.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;
        var view = car.AddComponent<View>();
        view.data = data;
        
        // ReSharper disable once PossibleNullReferenceException
        var pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, 0));
        pos.y = 2;
        car.transform.position = pos;

        // Create the text display
        var displayObject = new GameObject(data.Message);
        displayObject.transform.SetParent(car.transform, false);
        displayObject.transform.rotation = _cam.transform.rotation;
        var canvas = displayObject.AddComponent<Canvas>();
        view.uiCanvas = canvas;
        canvas.pixelPerfect = true;
        canvas.planeDistance = 10;
        canvas.transform.position = pos + new Vector3(0, 5, 0);
        canvas.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = _cam;

        var rect = canvas.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, -0.5f);
        rect.sizeDelta = new Vector2(50, 10);
        
        
        view.text = displayObject.AddComponent<Text>();
        view.text.text = data.Message;
        view.text.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
        view.text.alignByGeometry = true;
        view.text.verticalOverflow = VerticalWrapMode.Overflow;
        view.text.horizontalOverflow = HorizontalWrapMode.Overflow;
        view.text.alignment = TextAnchor.MiddleCenter;
        
        var scaler = displayObject.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 50;

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
        rb.AddForceAtPosition(inputVel, rb.position + transform.forward + new Vector3(0, 1, 0));
        transform.localScale = new Vector3(data.Scale, data.Scale, data.Scale);

        // Keep the UI canvas camera aligned
        uiCanvas.transform.rotation = _cam.transform.rotation;

        text.text = data.Message;
    }
}