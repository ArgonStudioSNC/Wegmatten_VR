using UnityEngine;

public class DeviceOrientationManager : MonoBehaviour
{
    #region PUBLIC_MEMBERS_VARIABLES

    [Tooltip("On left roll back to menu")]
    public bool backToMenu = true;

    [Range(0, 90)]
    public float leftRollAction = 45f;

    [Tooltip("On right roll switch AR-VR")]
    public bool switchMixedRealityMode = true;

    [Range(-90, 0)]
    public float rightRollAction = -45f;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBERS_VARIABLES

    private TransitionManager mTransitionManager;
    private Gyroscope m_Gyro;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTransitionManager = FindObjectOfType<TransitionManager>();

        if (!Input.gyro.enabled)
        {
            Debug.Log("No gyro available!");
        }

        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    void Update()
    {
        //GetComponent<BackToMenu>().GoToMenuPage();
        // mTransitionManager.Play(!mTransitionManager.InAR);
    }

    //TEST
    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("input.gyro.rotationRate: " + Input.gyro.rotationRate);
        GUILayout.Label("phone dimentions: " + Screen.width + " : " + Screen.height);
    }

    #endregion // MONOBEHAVIOUR_METHODS
}
