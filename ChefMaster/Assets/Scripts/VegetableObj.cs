using UnityEngine;

/// <summary>
/// 
/// Scriptable Object for vegetable 
/// Not used in current Main Scene, can be used if needed
/// 
/// </summary>

[CreateAssetMenu(fileName ="Vegetable", menuName ="Vegetable")]
public class VegetableObj : ScriptableObject
{
    public InventoryEnum vegetableType;
    public float choppingTime;
    public GameObject model;
    public GameObject ChoppedModel;
}
