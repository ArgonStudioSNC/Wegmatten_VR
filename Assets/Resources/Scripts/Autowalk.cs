using UnityEngine;

public class Autowalk : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    //This is the variable for the player speed
    [Tooltip("With this speed the player will move.")]
    public float speed;

    [Tooltip("Activate this checkbox if the player shall move when the Cardboard trigger is pulled.")]
    public bool walkWhenTriggered;

    [Tooltip("Activate this Checkbox if you want to freeze the y-coordiante for the player. " +
             "For example in the case of you have no collider attached to your CardboardMain-GameObject" +
             "and you want to stay in a fixed level.")]
    public bool freezeYPosition;

    [Tooltip("This is the fixed y-coordinate.")]
    public float yOffset;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    // This variable determinates if the player will move or not 
    private bool mIsWalking = false;
    private Transform mCamera = null;
    private TransitionManager mTransitionManager;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTransitionManager = FindObjectOfType<TransitionManager>();
        mCamera = Camera.main.transform;
    }

    void Update()
    {
        // Walking disabled in AR
        if (mTransitionManager.InAR)
        {
            mIsWalking = false;
            return;
        }

        // Check that no other action was intended
        RaycastHit hit;
        Ray cameraGaze = new Ray(mCamera.position, mCamera.forward);
        Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);

        // Walk when the Cardboard Trigger is used 
        if (walkWhenTriggered && Input.GetButtonDown("Fire1") && !hit.collider)
        {
            mIsWalking = !mIsWalking;
            Debug.Log("Trigger pulled!: isWalking=" + mIsWalking);
        }

        if (mIsWalking)
        {
            Vector3 direction = new Vector3(mCamera.transform.forward.x, 0, mCamera.transform.forward.z).normalized * speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
            transform.Translate(rotation * direction);
        }

        if (freezeYPosition)
        {
            transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS
}
