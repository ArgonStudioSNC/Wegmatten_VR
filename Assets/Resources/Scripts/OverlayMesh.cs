using UnityEngine;
using Vuforia;

/* Clip an overlay mesh to the clip plane to allow 2D object in stereoscopique mode.
 * 
 **/
public class OverlayMesh : MonoBehaviour
{
    #region PUBLIC_MEMBERS_VARIABLES

    public float meshScale = 0.012f; // relative to viewport

    #endregion // PUBLIC_MEMBERS_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Update()
    {
        Camera cam = DigitalEyewearARController.Instance.PrimaryCamera ?? Camera.main;
        if (cam.projectionMatrix.m00 > 0 || cam.projectionMatrix.m00 < 0)
        {
            // We adjust the reticle depth
            if (VideoBackgroundManager.Instance.VideoBackgroundEnabled)
            {
                // When the frustum skewing is applied (e.g. in AR view),
                // we shift the Reticle at the background depth,
                // so that the reticle appears in focus in stereo view
                BackgroundPlaneBehaviour bgPlane = cam.GetComponentInChildren<BackgroundPlaneBehaviour>();
                float bgDepth = bgPlane.transform.localPosition.z;
                if (bgDepth > cam.nearClipPlane)
                    this.transform.localPosition = Vector3.forward * bgDepth;
                else
                    this.transform.localPosition = Vector3.forward * (cam.nearClipPlane + 0.5f);
            }
            else
            {
                // if the frustum is not skewed, then we apply a default depth (which works nicely in VR view)
                this.transform.localPosition = Vector3.forward * (cam.nearClipPlane + 0.5f);
            }

            // We scale the mesh to be a small % of viewport
            float localDepth = this.transform.localPosition.z;
            float tanHalfFovX = 1.0f / cam.projectionMatrix[0, 0];
            float tanHalfFovY = 1.0f / cam.projectionMatrix[1, 1];
            float minTanFov = Mathf.Min(tanHalfFovX, tanHalfFovY);
            float minViewSize = 2 * minTanFov * localDepth;
            this.transform.localScale = new Vector3(meshScale * minViewSize, meshScale * minViewSize, 1);
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS
}
