using System;
using Cinemachine;
using UnityEngine;

namespace CameraControl
{
    [SaveDuringPlay]
    [RequireComponent(typeof(CinemachineFreeLook))]
    class CinemachineFreeLookZoom : MonoBehaviour
    {
        private CinemachineFreeLook _freelook;
        public CinemachineFreeLook.Orbit[] originalOrbits = Array.Empty<CinemachineFreeLook.Orbit>();
 
        [Tooltip("The minimum scale for the orbits")]
        [Range(0.01f, 1f)]
        public float minScale = 0.5f;

        [Tooltip("The maximum scale for the orbits")]
        [Range(1F, 5f)]
        public float maxScale = 1;

        [Tooltip("The Vertical axis.  Value is 0..1.  How much to scale the orbits")]
        [AxisStateProperty]
        public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "Mouse ScrollWheel", false);

        void OnValidate()
        {
            minScale = Mathf.Max(0.01f, minScale);
            maxScale = Mathf.Max(minScale, maxScale);
        }

        void Awake()
        {
            _freelook = GetComponentInChildren<CinemachineFreeLook>();
            if (_freelook != null && originalOrbits.Length == 0)
            {
                //Updates the input axis.
                zAxis.Update(Time.deltaTime);
                //Lerps the scale multiplier based on the axis value
                float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);
                // If we have reference to any orbits, set the freelook camera orbits to match
                for (int i = 0; i < Mathf.Min(originalOrbits.Length, _freelook.m_Orbits.Length); i++)
                {
                    _freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                    _freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
                }
            }
        }

        void Update()
        {
            if (_freelook != null)
            {
                //If we don't have the correct number of orbits cached, copy the orbits from the freelook camera into
                //a locally stored array with the starting values. This is so our scale multiplier references the initial value
                //instead of the current one.
                if (originalOrbits.Length != _freelook.m_Orbits.Length)
                {
                    originalOrbits = new CinemachineFreeLook.Orbit[_freelook.m_Orbits.Length];
                    Array.Copy(_freelook.m_Orbits, originalOrbits, _freelook.m_Orbits.Length);
                }
                //Update the axis value
                zAxis.Update(Time.deltaTime);
                //Lerp the scale multiplier baysed on the zAxis value
                float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);
                //Update the free look camera orbits to match the scaled version of the original
                for (int i = 0; i < Mathf.Min(originalOrbits.Length, _freelook.m_Orbits.Length); i++)
                {
                    _freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                    _freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
                }
            }
        }
    }
}