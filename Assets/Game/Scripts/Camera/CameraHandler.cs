using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace MumbaiChawls
{
    public class CameraHandler : Singleton<CameraHandler>
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 camFollowVelocity = Vector3.zero;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayer;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.1f;
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;

        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;

        [System.Obsolete]
        private void Awake()
        {
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayer = ~(1 << 8 | 1 << 9 | 1 << 10);
            targetTransform = FindObjectOfType<PlayerManager>().transform;  
        }

        public void followTarget(float delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position,ref camFollowVelocity, delta / followSpeed);
            myTransform.position = targetPosition;

            HandleCameraCollision(delta);
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;            
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;         
            
            pivotAngle = Mathf.Clamp(pivotAngle,minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            Quaternion lerpRotation = 
            cameraPivotTransform.localRotation = targetRotation;
        }

        public void HandleCameraCollision(float delta)
        {
            targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - myTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position,cameraSphereRadius,direction,out hit, Mathf.Abs(targetPosition), ignoreLayer))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(dis - cameraCollisionOffset);
            }
            if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }
            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }
    }
}

