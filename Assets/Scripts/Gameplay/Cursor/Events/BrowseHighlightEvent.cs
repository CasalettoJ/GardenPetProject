using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public static partial class GameEventLibrary { public static BrowseHighlightEvent BrowseHighlightEvent = new BrowseHighlightEvent(); }
    public class BrowseHighlightEvent : GameEvent<GameObjectEventArgs>{}
}

