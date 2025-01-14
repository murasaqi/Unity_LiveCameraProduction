﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if USE_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

#if USE_URP
using UnityEngine.Rendering.Universal;
#endif

#if USE_CINEMACHINE
#if USE_CINEMACHINE_3
using Unity.Cinemachine;
#else
using Cinemachine;
#endif
#endif


namespace CameraLiveProduction
{

    
    [Serializable]
    [ExecuteAlways]
    [RequireComponent(typeof(Camera))]
    public class LiveCamera:LiveCameraBase
    {
        public CameraMixer cameraMixer;
        public bool useCinemachineVolumeSettings = true;
        public CinemachineVolumeForceLayerChange cinemachineVolumeForceLayerChange;

#if USE_URP
        public UniversalAdditionalCameraData universalAdditionalCameraData;
#endif
        private void OnEnable()
        {
            Initialize();
        }

        [ContextMenu("Initialize")]
        public override void Initialize()
        {
            originalCamera = GetComponent<Camera>();
#if USE_CINEMACHINE
            cinemachineBrain = GetComponent<CinemachineBrain>();
            if (useCinemachineVolumeSettings&& cinemachineBrain)
            {
                cinemachineVolumeForceLayerChange = GetComponent<CinemachineVolumeForceLayerChange>();
                if (cinemachineVolumeForceLayerChange == null)
                {
                    cinemachineVolumeForceLayerChange = gameObject.AddComponent<CinemachineVolumeForceLayerChange>();
                }
            }
            else
            {
                cinemachineVolumeForceLayerChange = GetComponent<CinemachineVolumeForceLayerChange>();
                if(cinemachineVolumeForceLayerChange != null)
                    DestroyImmediate(cinemachineVolumeForceLayerChange);
            }
#endif

#if USE_HDRP
            hdAdditionalCameraData = GetComponent<HDAdditionalCameraData>();
#endif
#if USE_URP
            universalAdditionalCameraData = GetComponent<UniversalAdditionalCameraData>();
#endif
            
        }


        private void Update()
        {
            if (originalCamera == null)
            {
                originalCamera = GetComponent<Camera>();
            }

            // if (cinemachineVolumeForceLayerChange != null && cinemachineVolumeForceLayerChange.volume != null)
            // {
            //     cinemachineVolumeForceLayerChange.volume.gameObject.SetActive(TargetCamera.enabled);
            // }

           
        }

        private void OnDestroy()
        {
            if (cloneCamera != null)
            {
#if USE_HDRP
                hdAdditionalCameraData = null;
#endif
                if (cloneLiveCamera != null)
                {
                    
                }
                DestroyImmediate(cloneCamera);
            }
        }

        // public override Camera camera => targetCamera;
    }
}