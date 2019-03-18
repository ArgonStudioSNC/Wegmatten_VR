using UnityEngine;

public class ARtoVR : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    private TransitionManager mTransitionManager;
    #endregion //PRIVATE_MEMBERS

    #region PUBLIC_MEMBERS
    public GUISkin theGUISkin;
    #endregion //PUBLIC_MEMBERS

    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        mTransitionManager = FindObjectOfType<TransitionManager>();
    }

    void Update()
    {
        if (theGUISkin != null)
        {
        }
    }

    private void OnGUI()
    {
        AutoResize(1080, 1920); // ratio according to the base/current aspect

        int leftOffset = 35;
        int topOffset = 35;
        int space = 25;

        // button variables
        GUIStyle btnStyle = this.theGUISkin.customStyles[0];
        string btnText = "To VR";
        int btnHeight = 106;
        int btnWidth = 1010;

        // button
        if (GUI.Button(new Rect(leftOffset + btnWidth + space, topOffset, btnWidth, btnHeight), btnText, btnStyle)) ToVR();
    }
    #endregion // MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    public void ToVR()
    {
        bool goingToAR = false;
        mTransitionManager.Play(goingToAR);
    }
    #endregion //PUBLIC_METHODS

    #region PRIVATE_METHODS
    // function to resize the UI on a ratio compute from the working aspect and the device aspect
    private static void AutoResize(int screenWidth, int screenHeight)
    {
        float ratio = System.Math.Min((float)Screen.width / screenWidth, (float)Screen.height * screenHeight);
        Vector2 resizeRatio = new Vector2(ratio, ratio);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }
    #endregion //PRIVATE_METHODS
}
