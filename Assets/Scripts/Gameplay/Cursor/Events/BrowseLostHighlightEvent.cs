using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    // When the browser cursor has nothing to highlight anymore
    public static partial class GameEventLibrary { public static BrowseLostHighlightEvent BrowseLostHighlightEvent = new BrowseLostHighlightEvent(); }
    public class BrowseLostHighlightEvent : GameEvent<EmptyEventArgs>{}
}
