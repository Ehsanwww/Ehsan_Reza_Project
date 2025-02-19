﻿using System.Collections;
using UnityEngine;
using ImmersiveVRTools.Runtime.Common.Extensions;

namespace ImmersiveVRTools.Runtime.Common.Utilities
{
    public class TransformHolder
    {
        private Transform _keepTransformInPlace;
        private Vector3 _keepTransformInPlacePosition;
        private Quaternion _keepTransformInPlaceRotation;

        private bool _stopTransformFreeze;
        private int _runs = 0;
        private bool _isCoroutineRunning = false;

        public IEnumerator HoldTransformInPlace(Transform transform)
        {
            if (_isCoroutineRunning)
            {
                UnityEngine.Debug.Log($"Transform: '{transform.name}' already held in place, make sure to call Stop() first.");
                yield break;
            }
            
            _isCoroutineRunning = true;
            UnityEngine.Debug.Log($"Transform: '{transform.name}' will be held at current position/rotation.");
            
            _keepTransformInPlace = transform;
            _keepTransformInPlacePosition = transform.position;
            _keepTransformInPlaceRotation = transform.rotation;

            while (!_stopTransformFreeze)
            {
                _keepTransformInPlace.SetPosition(_keepTransformInPlacePosition);
                _keepTransformInPlace.SetRotation(_keepTransformInPlaceRotation);      
                
                yield return new WaitForFixedUpdate();

                if (_runs++ % 10000 == 0)
                {
                    UnityEngine.Debug.Log($"Transform: '{_keepTransformInPlace.name}' freezed for 10000 frames, make sure to call stop when appropriate.");
                }
            }

            UnityEngine.Debug.Log($"Transform: '{_keepTransformInPlace.name}' will be no longer held at current position/rotation.");
            _stopTransformFreeze = false;
            _isCoroutineRunning = false;
        }

        public void Stop()
        {
            _stopTransformFreeze = true;
        }
    }
}