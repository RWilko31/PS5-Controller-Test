using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InputManagerScript : MonoBehaviour
{
    PlayerInput PI;
    InputManager IM;

    #region ControllerContainer
    [Header("ControllerContainer")]
    [SerializeField] GameObject ps5Container;
    #endregion

    #region colours
    [Header("colours")]
    [SerializeField] Color enabledColour;
    [SerializeField] Color disabledColour;
    #endregion

    #region Lights
    [Header("Lights")]
    [SerializeField] GameObject Square;
    [SerializeField] GameObject Triangle;
    [SerializeField] GameObject Cross;
    [SerializeField] GameObject Circle;
    [SerializeField] GameObject Options;
    [SerializeField] GameObject Share;
    [SerializeField] GameObject dUp;
    [SerializeField] GameObject dDown;
    [SerializeField] GameObject dLeft;
    [SerializeField] GameObject dRight;
    [SerializeField] GameObject psButton;
    [SerializeField] GameObject mic;
    [SerializeField] GameObject touchpadButton;
    #endregion

    #region UI buttons
    [Header("UI buttons")]
    [SerializeField] GameObject R1;
    [SerializeField] GameObject R2;
    [SerializeField] GameObject R3;
    [SerializeField] GameObject L1;
    [SerializeField] GameObject L2;
    [SerializeField] GameObject L3;
    [SerializeField] GameObject Ljoystick;
    [SerializeField] GameObject Rjoystick;
    #endregion

    #region Ps5Input
    [Header("PS5 Input")]
    public bool ps5_circle;
    public bool ps5_triangle;
    public bool ps5_square;
    public bool ps5_cross;
    public bool ps5_dPad_Up;
    public bool ps5_dPad_Down;
    public bool ps5_dPad_Left;
    public bool ps5_dPad_Right;
    public bool ps5_options;
    public bool ps5_share;
    public bool ps5_touchpadButton;
    public bool ps5_home;
    public bool ps5_mic;
    public bool ps5_tp;
    public bool ps5_R1;
    public bool ps5_R2;
    public bool ps5_R3;
    public bool ps5_L1;
    public bool ps5_L2;
    public bool ps5_L3;
    public Vector2 LjoystickValue;
    public Vector2 RjoystickValue;
    #endregion

    private Vector3 LJP;
    private Vector3 RJP;

    private Vector2 rotation;
    private Vector3 defRot;


    private void OnEnable() { IM.PS5.Enable(); } //Enables user input 
    private void OnDisable() { IM.PS5.Disable(); } //Disables user input
    void Awake()
    {
        PI = GetComponent<PlayerInput>();
        IM = new InputManager();
        ps5_mic = false;
        LJP = Ljoystick.GetComponent<RectTransform>().position;
        RJP = Rjoystick.GetComponent<RectTransform>().position;
        defRot = ps5Container.transform.rotation.eulerAngles;
        Application.targetFrameRate = 90;
        BindInputs();
    }

    private void BindInputs()
    {
        IM.PS5.Circle.performed += ctx => { ps5_circle = true; };
        IM.PS5.Circle.canceled += ctx => { ps5_circle = false;};
        IM.PS5.Triangle.performed += ctx => { ps5_triangle = true; };
        IM.PS5.Triangle.canceled += ctx => { ps5_triangle = false;};
        IM.PS5.Square.performed += ctx => { ps5_square = true; };
        IM.PS5.Square.canceled += ctx => { ps5_square = false;};
        IM.PS5.Cross.performed += ctx => { ps5_cross = true; };
        IM.PS5.Cross.canceled += ctx => { ps5_cross = false;};
        IM.PS5.Start.performed += ctx => { ps5_options = true; };
        IM.PS5.Start.canceled += ctx => { ps5_options = false; };
        IM.PS5.Select.performed += ctx => { ps5_share = true; };
        IM.PS5.Select.canceled += ctx => { ps5_share = false; };
        IM.PS5.TouchpadButton.performed += ctx => { ps5_touchpadButton = true; };
        IM.PS5.TouchpadButton.canceled += ctx => { ps5_touchpadButton = false; };
        IM.PS5.PsButton.performed += ctx => { ps5_home = true; };
        IM.PS5.PsButton.canceled += ctx => { ps5_home = false; };
        IM.PS5.Dpad_Up.performed += ctx => { ps5_dPad_Up = true; };
        IM.PS5.Dpad_Up.canceled += ctx => { ps5_dPad_Up = false; };
        IM.PS5.Dpad_Down.performed += ctx => { ps5_dPad_Down = true; };
        IM.PS5.Dpad_Down.canceled += ctx => { ps5_dPad_Down = false; };
        IM.PS5.Dpad_Left.performed += ctx => { ps5_dPad_Left = true; };
        IM.PS5.Dpad_Left.canceled += ctx => { ps5_dPad_Left = false; };
        IM.PS5.Dpad_Right.performed += ctx => { ps5_dPad_Right = true; };
        IM.PS5.Dpad_Right.canceled += ctx => { ps5_dPad_Right = false; };
        IM.PS5.Mic.performed += ctx => { ps5_mic = !ps5_mic; };
        IM.PS5.R1.performed += ctx => { ps5_R1 = true; };
        IM.PS5.R1.canceled += ctx => { ps5_R1 = false; };
        IM.PS5.R2.performed += ctx => { ps5_R2 = true; };
        IM.PS5.R2.canceled += ctx => { ps5_R2 = false; };
        IM.PS5.R3.performed += ctx => { ps5_R3 = true; };
        IM.PS5.R3.canceled += ctx => { ps5_R3 = false; };
        IM.PS5.L1.performed += ctx => { ps5_L1 = true; };
        IM.PS5.L1.canceled += ctx => { ps5_L1 = false; };
        IM.PS5.L2.performed += ctx => { ps5_L2 = true; };
        IM.PS5.L2.canceled += ctx => { ps5_L2 = false; };
        IM.PS5.L3.performed += ctx => { ps5_L3 = true; };
        IM.PS5.L3.canceled += ctx => { ps5_L3 = false; };
        IM.PS5.LJoystick.performed += ctx => { LjoystickValue = ctx.ReadValue<Vector2>() * 5f; };
        IM.PS5.LJoystick.canceled += ctx => { LjoystickValue = Vector2.zero; };
        IM.PS5.RJoystick.performed += ctx => { RjoystickValue = ctx.ReadValue<Vector2>() * 5f; };
        IM.PS5.RJoystick.canceled += ctx => { RjoystickValue = Vector2.zero; };

        IM.PS5.Exit.performed += ctx => { Quit(); };
        IM.PS5.Rotation.performed += ctx => { rotation = ctx.ReadValue<Vector2>() * Time.deltaTime * 180; };
        IM.PS5.ResetRot.performed += ctx => { ps5Container.transform.rotation = Quaternion.Euler(defRot); };
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLights();
        UpdateUIButtons(); 
        UpdateRotation();
        if (ps5_share && ps5_options && ps5_R2 && ps5_L2) Application.Quit();
    }
    public void Quit()
    { Application.Quit(); }
    private void UpdateLights()
    {
        Triangle.SetActive(ps5_triangle);
        Square.SetActive(ps5_square);
        Circle.SetActive(ps5_circle);
        Cross.SetActive(ps5_cross);
        dUp.SetActive(ps5_dPad_Up);
        dDown.SetActive(ps5_dPad_Down);
        dLeft.SetActive(ps5_dPad_Left);
        dRight.SetActive(ps5_dPad_Right);
        Options.SetActive(ps5_options);
        Share.SetActive(ps5_share);
        touchpadButton.SetActive(ps5_touchpadButton);
        psButton.SetActive(ps5_home);
        mic.SetActive(ps5_mic);
    }
    private void UpdateUIButtons()
    {
        R1.GetComponent<Image>().color = GetColour(ps5_R1);
        R2.GetComponent<Image>().color = GetColour(ps5_R2);
        R3.GetComponent<Image>().color = GetColour(ps5_R3);
        L1.GetComponent<Image>().color = GetColour(ps5_L1);
        L2.GetComponent<Image>().color = GetColour(ps5_L2);
        L3.GetComponent<Image>().color = GetColour(ps5_L3);
        Rjoystick.GetComponent<Image>().color = GetColour(ps5_R3);
        Ljoystick.GetComponent<Image>().color = GetColour(ps5_L3);
        Ljoystick.GetComponent<RectTransform>().position = new Vector3(LJP.x + LjoystickValue.x, LJP.y + LjoystickValue.y, LJP.z);
        Rjoystick.GetComponent<RectTransform>().position = new Vector3(RJP.x + RjoystickValue.x, RJP.y + RjoystickValue.y, RJP.z);
    }
    private Color GetColour(bool state)
    {
        if (state) return enabledColour;
        else return disabledColour;
    }
    private void UpdateRotation()
    {
        Vector3 rot = ps5Container.transform.rotation.eulerAngles;
        ps5Container.transform.rotation = Quaternion.Euler(rot.x + rotation.y, rot.y + rotation.x, rot.z);
    }
}
