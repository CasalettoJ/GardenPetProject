using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class BrowseHighlightItem : MonoBehaviour
    {
        private Transform _cursorTop;
        private Transform _cursorTopParentDefault;
        private Vector3 _resetPoint = Vector3.zero;
        private bool _isHighlighted = false;

        // Start is called before the first frame update
        private void Start()
        {
            _cursorTop = transform;
            _resetPoint = _cursorTop.transform.localPosition;
            _cursorTopParentDefault = _cursorTop.parent;
        }

        private void OnEnable()
        {
            GameEventLibrary.BrowseHighlightEvent.RegisterListener(OnBrowserHighlight);
            GameEventLibrary.BrowseLostHighlightEvent.RegisterListener(OnBrowserLostHighlight);
        }

        private void OnDisable()
        {
            GameEventLibrary.BrowseHighlightEvent.UnregisterListener(OnBrowserHighlight);
            GameEventLibrary.BrowseLostHighlightEvent.UnregisterListener(OnBrowserLostHighlight);
        }

        private void OnDestroy()
        {
            GameEventLibrary.BrowseHighlightEvent.UnregisterListener(OnBrowserHighlight);
            GameEventLibrary.BrowseLostHighlightEvent.UnregisterListener(OnBrowserLostHighlight);
        }

        private void Update()
        {
            if(_isHighlighted)
            {
                _cursorTop.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnBrowserHighlight(GameObjectEventArgs browserHighlight)
        {
            Highlightable objHighlight = browserHighlight.GameObject.GetComponent<Highlightable>();
            if (objHighlight != null)
            {
                _cursorTop.parent = browserHighlight.GameObject.transform;
                _cursorTop.localPosition = objHighlight.HighlightPoint;
                _isHighlighted = true;
            }
        }

        private void OnBrowserLostHighlight(EmptyEventArgs args)
        {
            ResetHighlighter();
        }

        private void ResetHighlighter()
        {
            _cursorTop.parent = _cursorTopParentDefault;
            _cursorTop.transform.localPosition = _resetPoint;
            _cursorTop.rotation = Quaternion.Euler(0,0,0);
            _isHighlighted = false;
        }
    }
}
