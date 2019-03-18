using UnityEngine;

/* This class is for Unity Editor. It can be disabled at compilation.
 * 
 **/
public class EditorCameraController : MonoBehaviour
{
    #region PUBLIC_MEMBERS_VARIABLES

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float minimumZ = -90F;
    public float maximumZ = 90F;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBERS_VARIABLES

    private float rotationX = 0F;
    private float rotationY = 0F;
    private float rotationZ = 0F;
    private Quaternion originalRotation;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                rotationZ += Input.GetAxis("Mouse X") * sensitivityX;
                rotationZ = ClampAngle(rotationZ, minimumZ, maximumZ);
            }

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            Quaternion zQuaternion = Quaternion.AngleAxis(rotationZ, Vector3.forward);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion * zQuaternion;
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    #endregion // PUBLIC_METHODS
}
