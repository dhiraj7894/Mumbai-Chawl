using MumbaiChawls.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Control
{

    public class ActivateEmote : Interactable
    {
        public Transform playerStandingPosition;
        public List<string> AnimationToPlaye;
        public override void Interact(PlayerManager manager)
        {
            base.Interact(manager);
            manager.InteractWithFirecracker(playerStandingPosition, AnimationToPlaye[Random.Range(0, AnimationToPlaye.Count - 1)]);

            Vector3 rotateDirection = transform.position - manager.transform.position;
            rotateDirection.y = 0;
            rotateDirection.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotateDirection);
            Quaternion rotateTowards = Quaternion.Slerp(manager.transform.rotation, tr, 300*Time.deltaTime);
            manager.transform.rotation = rotateTowards;

        }
    }
}
