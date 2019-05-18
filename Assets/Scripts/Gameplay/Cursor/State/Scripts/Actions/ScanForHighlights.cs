using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName = "ScanForHighlights", menuName = "StateMachines/Cursor/Actions/ScanForHighlights")]
    public class ScanForHighlights : StateAction<CursorStateController>
    {
        [Header("Settings")]
        public float browseRaycastWidth = 0.75f;

        public override void Act(CursorStateController controller)
        {
            Vector3 pos = controller.transform.position;
            if (pos != controller.lastPos)
            {
                controller.lastPos = pos;
                Scan(controller);
            }
        }

        private void Scan(CursorStateController controller)
        {
            Collider[] colliders = Physics.OverlapSphere(controller.lastPos, browseRaycastWidth);

            if (colliders.Length == 0)
            {
                if (controller.highlightedObj != null)
                {
                    LostHighlight(controller);
                }
                return;
            }

            float shortestDistance = int.MaxValue;
            Highlightable closestHighlightableObject = null;
            foreach (Collider collider in colliders)
            {
                // TODO: Find distance between collider and transform, find closest highlight, go to it instead.
                Highlightable highlightable = collider.gameObject.GetComponent<Highlightable>();
                float distance = Vector3.Distance(collider.transform.position, controller.lastPos);
                if (highlightable != null && distance < shortestDistance)
                {
                    closestHighlightableObject = highlightable;
                    shortestDistance = distance;
                }
            }
            if (closestHighlightableObject != null && closestHighlightableObject.gameObject != controller.highlightedObj)
            {
                GainedHighlight(controller, closestHighlightableObject.gameObject);
            }
            else if (closestHighlightableObject == null && controller.highlightedObj != null)
            {
                LostHighlight(controller);
            }
        }

        private void GainedHighlight(CursorStateController controller, GameObject hit)
        {
            GameEventLibrary.BrowseHighlightEvent.FireEvent(new GameObjectEventArgs(hit));
        }

        private void LostHighlight(CursorStateController controller)
        {
            GameEventLibrary.BrowseLostHighlightEvent.FireEvent(new EmptyEventArgs());
        }
    }
}