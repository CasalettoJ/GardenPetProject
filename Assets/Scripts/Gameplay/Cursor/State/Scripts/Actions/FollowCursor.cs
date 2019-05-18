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

        public override void Act(CursorStateController stateController)
        {
            CastCursorToGround(stateController);
        }

        private void CastCursorToGround(CursorStateController stateController)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask(cursorLayer)))
            {
                stateController.m_transform.position = hit.point + new Vector3(0, yOffset, 0);
            }
        }
    }
}