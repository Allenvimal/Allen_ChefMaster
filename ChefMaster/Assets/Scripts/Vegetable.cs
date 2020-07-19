using UnityEngine;

/// <summary>
/// 
/// This class defines the properties of a vegetable
/// Used in vegetable prefab 
/// 
/// </summary>

public class Vegetable : MonoBehaviour
{
    public InventoryEnum vegetableType;
    public float choppingTime;
    public GameObject model;
    public GameObject ChoppedModel;
}
