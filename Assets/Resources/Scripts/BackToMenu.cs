using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    #region PRIVATE_MEMBERS_VARIABLES

    private const float DOUBLE_TAP_MAX_DELAY = 0.5f;//seconds
    private int mTapCount = 0;
    private float mTimeSinceLastTap = 0;

    #endregion //PRIVATE_MEMBERS_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTapCount = 0;
        mTimeSinceLastTap = 0;
    }

    void Update()
    {
#if UNITY_ANDROID
        // On Android, the Back button is mapped to the Esc key
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoToMenuPage();
        }
#else
        HandleTap();
#endif
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void GoToMenuPage()
    {
        Debug.Log("Loading Menu scene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS

    private void HandleTap()
    {
        if (mTapCount == 1)
        {
            mTimeSinceLastTap += Time.deltaTime;
            if (mTimeSinceLastTap > DOUBLE_TAP_MAX_DELAY)
            {
                // reset touch count and timer
                mTapCount = 0;
                mTimeSinceLastTap = 0;
            }
        }
        else if (mTapCount == 2)
        {
            // we got a double tap
            OnDoubleTap();

            // reset touch count and timer
            mTimeSinceLastTap = 0;
            mTapCount = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mTapCount++;
        }
    }

    protected virtual void OnDoubleTap()
    {
        GoToMenuPage();
    }

    #endregion //PRIVATE_METHODS
}
