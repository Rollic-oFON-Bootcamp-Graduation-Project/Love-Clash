using UnityEngine;
using UnityEngine.Events;

public class Observer
{
    public static UnityAction<int> WeaponUpdate;
    public static UnityAction<Collectable> AddToStack;
    public static UnityAction<Collectable> RemoveFromStack;

}
