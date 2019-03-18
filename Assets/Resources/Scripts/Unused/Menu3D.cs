/*===============================================================================
Copyright (c) 2015-2018 PTC Inc. All Rights Reserved.

Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using Vuforia;

public class Menu3D : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    [Range(0, 2)]
    public float fadeDuration = 1.6f;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private float mButtonAlpha = 0;
    private Camera m_Camera;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    void Update()
    {
        if (m_Camera != null)
        {
            // Update position of ARButton to always be about 2 meters below the camera
            Vector3 camPos = m_Camera.transform.position;
            Vector3 camForwardDir = m_Camera.transform.forward;
            Vector3 camUpDir = m_Camera.transform.up;
            Vector3 lookUpDir = (camUpDir + camForwardDir) / 2;
            Vector3 forwardDir = new Vector3(lookUpDir.x, 0, lookUpDir.z);
            forwardDir.Normalize();

            transform.position = camPos - 1.5f * Vector3.up + forwardDir;

            // Apply rotation and scale
            Vector3 toCameraVec = camPos - transform.position;
            float camDist = toCameraVec.magnitude;
            if (camDist > m_Camera.nearClipPlane)
            {
                // Orient the button surface to face the viewer (like a billboard)
                transform.rotation = Quaternion.LookRotation(m_Camera.transform.forward, m_Camera.transform.up);
            }
        }
    }

    void LateUpdate()
    {
        if (m_Camera != null)
        {
            Vector2 vp = m_Camera.WorldToViewportPoint(transform.position);
            if (vp.x > 0.2f && vp.x < 0.8f && vp.y > 0.2f && vp.y < 0.8f)
            {
                if (mButtonAlpha < 1)
                    mButtonAlpha += Time.deltaTime / fadeDuration;
            }
            else
            {
                if (mButtonAlpha > 0)
                    mButtonAlpha -= Time.deltaTime / fadeDuration;
            }

            mButtonAlpha = Mathf.Clamp01(mButtonAlpha);
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(1.0f, 1.0f, 1.0f, mButtonAlpha));
            }
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region VUFORIA_CALLBACKS

    void OnVuforiaStarted()
    {
        m_Camera = DigitalEyewearARController.Instance.PrimaryCamera ?? Camera.main;
    }

    #endregion // VUFORIA_CALLBACKS
}
