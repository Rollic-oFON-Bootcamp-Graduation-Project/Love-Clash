using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Observer
{
    public static UnityAction<int> WeaponUpdate;
    public static UnityAction<int> PlayerUpdate;
    public static UnityAction PlayerAnimationChange;

    public static UnityAction<Collectable> AddToStack;
    public static UnityAction<Collectable> RemoveFromStack;
    public static UnityAction<Vector3, Vector3, List<Collectable>> StackHandleBattle;


}
