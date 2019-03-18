using UnityEngine;
using System.Collections;

/* Offers different actions for 3D buttons
 * 
 * */
public class Menu3DManager : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public enum TriggerType
    {
        VR_SWITCH_MODEL,
        VR_BACKTO_MENU,
        VR_TO_AR,
        AR_SWITCH_MODEL,
        AR_ENABLE_MODEL
    }

    public TriggerType triggerType = TriggerType.VR_SWITCH_MODEL;
    public GameObject[] Models;
    public Material[] Materials;
    public bool modelEnabled = true;
    public bool Focused { get; set; }

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private bool mTriggered;
    private int mModelSize;
    private int mMaterialSize;
    private int mCurrentModelID;
    private int mCurrentMaterialID;
    private Transform mCameraTransform;
    private TransitionManager mTransitionManager;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mCameraTransform = Camera.main.transform;
        mTransitionManager = FindObjectOfType<TransitionManager>();
        mModelSize = Models.Length;
        mMaterialSize = Materials.Length;
        mCurrentModelID = 0;
        mCurrentMaterialID = 0;

        foreach (GameObject obj in Models)
        {
            obj.SetActive(false);
        }

        switch (triggerType)
        {
            case TriggerType.VR_SWITCH_MODEL:
                Models[mCurrentModelID].SetActive(true);
                break;
            case TriggerType.AR_SWITCH_MODEL:
                Models[mCurrentModelID].SetActive(true);
                break;
            case TriggerType.AR_ENABLE_MODEL:
                if (modelEnabled) Models[mCurrentModelID].SetActive(true);
                break;
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray cameraGaze = new Ray(mCameraTransform.position, mCameraTransform.forward);
        Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);
        Focused = hit.collider && (hit.collider.gameObject == gameObject);

        if (mTriggered)
            return;

        bool startAction = Input.GetButtonDown("Fire1");

        if (Focused && startAction)
        {
            mTriggered = true;
            Debug.Log("Trigger pulled! : " + gameObject.name + " selected.");

            switch (triggerType)
            {
                case TriggerType.VR_SWITCH_MODEL:
                    VRSwitchModel();
                    break;
                case TriggerType.VR_BACKTO_MENU:
                    VRBacktoMenu();
                    break;
                case TriggerType.VR_TO_AR:
                    VRtoAR();
                    break;
                case TriggerType.AR_SWITCH_MODEL:
                    ARSwitchModel();
                    break;
                case TriggerType.AR_ENABLE_MODEL:
                    AREnableModel();
                    break;
            }
            StartCoroutine(ResetAfter(0.5f));
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS

    #region PRIVATE_METHODS

    private void VRSwitchModel()
    {
        foreach (GameObject obj in Models)
        {
            obj.SetActive(false);
        }

        mCurrentModelID = (mCurrentModelID + 1) % mModelSize;
        Models[mCurrentModelID].SetActive(true);

        Debug.Log("Active model: " + Models[mCurrentModelID].name);
    }

    private void VRBacktoMenu()
    {
        Debug.Log("Loading Menu scene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("0-Menu");
    }

    private void VRtoAR()
    {
        Debug.Log("Launching VR to AR transition");
        bool goingBackToAR = true;
        mTransitionManager.Play(goingBackToAR);
    }

    private void ARSwitchModel()
    {
        foreach (GameObject obj in Models)
        {
            obj.SetActive(false);
        }

        mCurrentModelID = (mCurrentModelID + 1) % mModelSize;
        Models[mCurrentModelID].SetActive(true);

        mCurrentMaterialID = (mCurrentMaterialID + 1) % mMaterialSize;
        UpdateMaterial(Materials[mCurrentMaterialID]);

        Debug.Log("Active model: " + Models[mCurrentModelID].name);
    }

    private void AREnableModel()
    {
        modelEnabled = !modelEnabled;
        Models[0].SetActive(false);
        if (modelEnabled)
        {
            Models[0].SetActive(true);
        }

        mCurrentMaterialID = (mCurrentMaterialID + 1) % mMaterialSize;
        UpdateMaterial(Materials[mCurrentMaterialID]);

        Debug.Log("Model " + Models[mCurrentModelID].name + "enabled=" + modelEnabled);
    }

    private void UpdateMaterial(Material mat)
    {
        Renderer meshRenderer = GetComponent<Renderer>();

        meshRenderer.material = mat;
    }

    private IEnumerator ResetAfter(float seconds)
    {
        Debug.Log("Resetting View trigger after: " + seconds);
        yield return new WaitForSeconds(seconds);

        // Reset variables
        mTriggered = false;
        Focused = false;
    }

    #endregion // PRIVATE_METHODS
}
