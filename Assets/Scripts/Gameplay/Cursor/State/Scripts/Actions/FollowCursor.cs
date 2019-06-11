using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName="FollowCursor", menuName = "StateMachines/Cursor/Actions/FollowCursor")]
    public class FollowCursor : StateAction<CursorStateController>
    {
        [Header("Settings")]
        public string cursorLayer = "Cursor_Layer";
        public float yOffset = 0.025f;
        public float raycastLength = 300.0f;

        public override void Act(CursorStateController stateController)
        {
            CastCursorToGround(stateController);
        }

        private void CastCursorToGround(CursorStateController stateController)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raycastLength, LayerMask.GetMask(cursorLayer)))
            {
                stateController.m_transform.position = hit.point + new Vector3(0, yOffset, 0);
            }
        }
    }
}