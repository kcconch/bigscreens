using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "cars", menuName = "Cars List")]
public class CarsList : ScriptableObject
{
    public List<GameObject> Cars;
}
