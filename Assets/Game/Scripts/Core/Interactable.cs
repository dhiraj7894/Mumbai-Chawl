using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Core
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactbleText;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }


        public virtual void Interact(PlayerManager manager)
        {
            Debug.Log("You have interacted with thi obj sob sob");
        }
    }
}
