using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class CursorDisplayHighlightItem : MonoBehaviour
    {
        private Transform _cursorDisplay;
        private Transform _cursorDisplayParentDefault;
        private Vector3 _resetPoint = Vector3.zero;
        private bool _isHighlighted = false;

        // Start is called before the first frame update
        private void Start()
        {
            _cursorDisplay = transform;
            _resetPoint = _cursorDisplay.transform.localPosition;
            _cursorDisplayParentDefault = _cursorDisplay.parent;
        }

        private void OnEnable()
        {
            GameEventLibrary.CursorLockedOnEvent.RegisterListener(OnBrowserHighlight);
            GameEventLibrary.CursorLostLockOnEvent.RegisterListener(OnBrowserLostHighlight);
        }

        private void OnDisable()
        {
            GameEventLibrary.CursorLockedOnEvent.UnregisterListener(OnBrowserHighlight);
            GameEventLibrary.CursorLostLockOnEvent.UnregisterListener(OnBrowserLostHighlight);
        }

        private void Update()
        {
            if (_isHighlighted)
            {
                _cursorDisplay.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnBrowserHighlight(GameObjectEventArgs browserHighlight)
        {
            Highlightable objHighlight = browserHighlight.GameObject.GetComponent<Highlightable>();
            if (objHighlight != null)
            {
                _cursorDisplay.parent = browserHighlight.GameObject.transform;
                _cursorDisplay.localPosition = Vector3.zero;
                _isHighlighted = true;
            }
        }

        private void OnBrowserLostHighlight(EmptyEventArgs args)
        {
            ResetHighlighter();
        }

        private void ResetHighlighter()
        {
            _cursorDisplay.parent = _cursorDisplayParentDefault;
            _cursorDisplay.transform.localPosition = _resetPoint;
            _cursorDisplay.rotation = Quaternion.Euler(0, 0, 0);
            _isHighlighted = false;
        }
    }
}
