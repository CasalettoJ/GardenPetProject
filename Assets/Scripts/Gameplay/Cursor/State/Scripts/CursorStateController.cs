using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class CursorStateController : StateController<CursorStateController>
    {
        [Header("Cursors")]
        public GameObject browseCursor;
        public GameObject lockOnCursor;


        // State Variables
        [HideInInspector]
        public Vector3 lastPos = Vector3.zero; // Last position of the cursor while browsing
        [HideInInspector]
        public Vector3 originalCursorTopLocalPos = Vector3.zero; // last localpos of the top cursor if a state will allow it to move
        public GameObject highlightedObj { get; private set; } // object if a game event in an action has highlighted it from the cursor

        public void Awake()
        {
            GameEventLibrary.BrowseHighlightEvent.RegisterListener(OnBrowserHighlight);
            GameEventLibrary.BrowseLostHighlightEvent.RegisterListener(OnBrowserLostHighlight);
        }

        public void OnDestroy()
        {
            GameEventLibrary.BrowseHighlightEvent.UnregisterListener(OnBrowserHighlight);
            GameEventLibrary.BrowseLostHighlightEvent.UnregisterListener(OnBrowserLostHighlight);
        }

        private void OnBrowserHighlight(GameObjectEventArgs browserHighlight)
        {
            highlightedObj = browserHighlight.GameObject;
        }

        private void OnBrowserLostHighlight(EmptyEventArgs args)
        {
            highlightedObj = null;
        }
    }
}