using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    // When the browser cursor locks on to a highlightable
    public static partial class GameEventLibrary { public static CursorLockedOnEvent CursorLockedOnEvent = new CursorLockedOnEvent(); }
    public class CursorLockedOnEvent : GameEvent<GameObjectEventArgs> { }
}