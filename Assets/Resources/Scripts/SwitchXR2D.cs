using UnityEngine;
using UnityEngine.UI;

public class SwitchXR2D : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Text myText;

    #endregion //PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private TransitionManager mTransitionManager;

    #endregion //PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTransitionManager = FindObjectOfType<TransitionManager>();
    }

    void Update()
    {
        if (mTransitionManager.InAR)
            myText.text = "VR";
        else
            myText.text = "AR";
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void SwitchXR()
    {
        if (mTransitionManager.InAR)
            mTransitionManager.Play(false);
        else
            mTransitionManager.Play(true);
    }

    #endregion // PUBLIC_METHODS
}
