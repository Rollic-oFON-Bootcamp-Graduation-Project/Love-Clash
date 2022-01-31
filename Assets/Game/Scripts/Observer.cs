using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Observer
{
    //These events are for updating weapon and player.
    public static UnityAction<int> WeaponUpdate;
    public static UnityAction<int> PlayerUpdate;

    //This event is for starting the game.
    //We are setting player animations etc, can add UI to this event too
    public static UnityAction StartGame;
    //Event that occurs when starting the battle.
    public static UnityAction StartBattle;
    //Event that occurs when ending the battle.
    public static UnityAction StopBattle;
    //This is for setting random collectable positions on arena.
    public static UnityAction PreBattle;


    public static UnityAction SetActiveArena;
    //Removes collectable from arena's reach.
    public static UnityAction<Collectable> RemoveFromArena;
    //Event for adding a collectable to the stack.
    public static UnityAction<Collectable> AddToStack;
    //Sets collectables position from stack to the points that are randomly created positions by ArenaSetPositions.
    public static UnityAction<List<Vector3>, List<Collectable>> StackHandleBattle;

    //Removes a collectable from stack and returns it
    public delegate Collectable RemoveFromStackDelegate();
    public static RemoveFromStackDelegate RemoveFromStack;

}
