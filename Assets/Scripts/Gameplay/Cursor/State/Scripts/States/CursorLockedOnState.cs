using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName = "CursorLockedOnState", menuName = "StateMachines/Cursor/States/CursorLockedOnState")]
    public class CursorLockedOnState : State<CursorStateController>
    {
        public override void OnEnter(CursorStateController controller)
        {
            GameEventLibrary.CursorLockedOnEvent.FireEvent(new GameObjectEventArgs(controller.highlightedObj));
            controller.lockOnCursor.SetActive(true);
            controller.originalCursorTopLocalPos = controller.lockOnCursor.transform.localPosition;
            controller.lockOnCursor.transform.SetParent(controller.highlightedObj.transform);
            Highlightable highlightComponent = controller.highlightedObj.GetComponent<Highlightable>();
            if (controller.highlightedObj != null)
            {
                controller.lockOnCursor.transform.localPosition = highlightComponent.HighlightPoint;
            }
            controller.m_transform.position = new Vector3(controller.highlightedObj.transform.position.x, controller.m_transform.position.y , controller.highlightedObj.transform.position.z);
        }

        public override void OnExit(CursorStateController controller)
        {
            GameEventLibrary.CursorLostLockOnEvent.FireEvent(new EmptyEventArgs());
            controller.lockOnCursor.transform.parent = controller.transform;
            controller.lockOnCursor.transform.localPosition = controller.originalCursorTopLocalPos;
            controller.lockOnCursor.SetActive(false);
        }
    }
}
