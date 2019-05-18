using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    [CreateAssetMenu(fileName = "CursorBrowseState", menuName = "StateMachines/Cursor/States/CursorBrowseState")]
    public class CursorBrowseState : State<CursorStateController>
    {
        public override void OnEnter(CursorStateController controller)
        {
            controller.browseCursor.SetActive(true);
        }

        public override void OnExit(CursorStateController controller)
        {
            controller.browseCursor.SetActive(false);
        }
    }
}
