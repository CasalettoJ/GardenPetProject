using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{
    public class Highlightable : MonoBehaviour
    {
        public Vector3 HighlightPoint;
        public Collider Collider;

        private void Start()
        {
            if(Collider == null)
            {
                Collider = GetComponent<Collider>();
            }
        }
    }
}

