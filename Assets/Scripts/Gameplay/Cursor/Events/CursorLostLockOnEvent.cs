using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PetProject
{
    // When the lockedon cursor stops being locked on
    public static partial class GameEventLibrary { public static CursorLostLockOnEvent CursorLostLockOnEvent = new CursorLostLockOnEvent(); }
    public class CursorLostLockOnEvent : GameEvent<EmptyEventArgs> { }
}