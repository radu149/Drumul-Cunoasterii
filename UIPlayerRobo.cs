    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class UIPlayerRobo : MonoBehaviour
    {
        public TMP_InputField commandInputField;
        public TextMeshProUGUI outputText;
        public ThirdPersonController MovementManagerPlayer;
        public PlayerDataPlace PlayerDataPlace;
        public Image PageManualUtilizare;
        public TextMeshProUGUI ManualText;
        public Camera CameraMaleM;
        public Camera CameraFemaleF;
        public Camera CameraView1;
        public Camera CameraView2;
        public Camera CameraView3;
//        public Button View1;
//        public Button View2;
//        public Button View3;
//        public Button PlayerButtonCamera;
        public GameObject Robot;
        public GameObject Platform;
        public StartScriptLevel2 StartScriptLevel2;

        void Start()
        {
            ToggleMenuRobot(false);
            ToggleMenuButtons(false);
//            View1.onClick.AddListener(OnButtonPressed1R);
//            View2.onClick.AddListener(OnButtonPressed2R);
//            View3.onClick.AddListener(OnButtonPressed3R);
//            PlayerButtonCamera.onClick.AddListener(OnButtonPressed4R);

            Display.displays[0].Activate();

            CameraView1.targetDisplay = 0;
            CameraView2.targetDisplay = 0;
            CameraView3.targetDisplay = 0;
            CameraMaleM.targetDisplay = 0;
            CameraFemaleF.targetDisplay = 0;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.M) && StartScriptLevel2.UIRobotWorking)
            {
                ToggleMenuRobot(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                MovementManagerPlayer.OptionsMovementNull = false;
                ToggleMenuButtons(true);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                ToggleMenuRobot(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                MovementManagerPlayer.OptionsMovementNull = true;
                ToggleMenuButtons(false);           
            }

            if (Input.GetKeyDown(KeyCode.F11))
            {
                Debug.Log("Screen: " + Screen.width + "x" + Screen.height + " Fullscreen: " + Screen.fullScreenMode);
                Debug.Log("Active Camera: " + GetActiveCamera().name);
            }
        }

        void ToggleMenuRobot(bool show)
        {
            commandInputField.gameObject.SetActive(show);
            outputText.gameObject.SetActive(show);
            PageManualUtilizare.gameObject.SetActive(show);
            ManualText.gameObject.SetActive(show);
        }

        void ToggleMenuButtons(bool show)
        {
//            View1.gameObject.SetActive(show);
//            View2.gameObject.SetActive(show);
//            View3.gameObject.SetActive(show);
//            PlayerButtonCamera.gameObject.SetActive(show);
        }

        void OnButtonPressed1R()
        {
            CameraView1.gameObject.SetActive(true);
            CameraView2.gameObject.SetActive(false);
            CameraView3.gameObject.SetActive(false);
            CameraMaleM.gameObject.SetActive(false);
            CameraFemaleF.gameObject.SetActive(false);
            GameObjectsAciveDebugTest();
        }

        void OnButtonPressed2R()
        {
            CameraView1.gameObject.SetActive(false);
            CameraView2.gameObject.SetActive(true);
            CameraView3.gameObject.SetActive(false);
            CameraMaleM.gameObject.SetActive(false);
            CameraFemaleF.gameObject.SetActive(false);
            GameObjectsAciveDebugTest();
        }

        void OnButtonPressed3R()
        {
            CameraView1.gameObject.SetActive(false);
            CameraView2.gameObject.SetActive(false);
            CameraView3.gameObject.SetActive(true);
            CameraMaleM.gameObject.SetActive(false);
            CameraFemaleF.gameObject.SetActive(false);
            GameObjectsAciveDebugTest();
        }

        void OnButtonPressed4R()
        {
            if(PlayerDataPlace.Instance.Male)
            {
                CameraMaleM.gameObject.SetActive(true);
                CameraView1.gameObject.SetActive(false);
                CameraView2.gameObject.SetActive(false);
                CameraView3.gameObject.SetActive(false);
                GameObjectsAciveDebugTest();
            }
            else if(PlayerDataPlace.Instance.Female)
            {
                CameraFemaleF.gameObject.SetActive(true);
                CameraView1.gameObject.SetActive(false);
                CameraView2.gameObject.SetActive(false);
                CameraView3.gameObject.SetActive(false);
                GameObjectsAciveDebugTest();
            }
        }

        Camera GetActiveCamera()
        {   
            if (CameraView1.gameObject.activeSelf) return CameraView1;
            if (CameraView2.gameObject.activeSelf) return CameraView2;
            if (CameraView3.gameObject.activeSelf) return CameraView3;
            if (CameraMaleM.gameObject.activeSelf) return CameraMaleM;
            if (CameraFemaleF.gameObject.activeSelf) return CameraFemaleF;
            return null;
        }

        void GameObjectsAciveDebugTest()
        {
            Platform.SetActive(true);
            Robot.SetActive(true);
        }
    }
