using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class View : MonoBehaviour
{
    private static List<GameObject> Cars;

    private static int CarIndex = 0;

    private static float Scale = 10;

    public static View Create()
    {
        if (Cars == null)
        {
            var CarsList = Resources.Load<CarsList>("cars");
            if (CarsList == null) throw new Exception("Could not find cars in Resources folder");
            Cars = CarsList.Cars;
        }

        var views = GameObject.Find("Views");
        if (views == null)
        {
            views = new GameObject("Views");
            views.transform.localScale = new Vector3(Scale, Scale, Scale);

        }

        var car = Instantiate(Cars[CarIndex++ % Cars.Count], views.transform, false);
        // var rb = car.AddComponent<Rigidbody>();
        var view = car.AddComponent<View>();
        return view;
    }

    public ClientData data;


    private void Update()
    {
        /*
        var newPos = Camera.main.ViewportToWorldPoint(data.Pos);
        newPos.z = 0;
        transform.localPosition = newPos;
        transform.localScale = new Vector3(data.Scale, data.Scale, 1);
        */
    }
}