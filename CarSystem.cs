using UnityEngine;

public class CarSystem : MonoBehaviour
{
    [Header("Objects1")]
    public GameObject Car1, Car2, Car3, Car4, Car5, Car6;
    [Header("Objects2")]
    public GameObject Car1B, Car2B, Car3B, Car4B, Car5B, Car6B;

    [Header("Essentials")]
    public float VitezaCarPoz = 15f;
    public float VitezaCarNeg = -15f;
    public float targetXPos = 100f;
    public float targetXNeg = -100f;
    private Vector3 initialPosition = new Vector3(-100f, 1.5f, -2.5f);
    private Vector3 Final1Position = new Vector3(100f, 1.5f, 2f);
    private Vector3 Position1 = new Vector3(100f, 1.5f, -2.5f);
    private Vector3 Position2 = new Vector3(-100f, 1.5f, -2.5f);
    public float TargetLightsX = -15f;
    public bool TextDialogueDone = false;
    private int car4PassCount = 0;

    private bool CarShouldStop = false;
    private bool hasActivatedLightCycle = false;

    public bool CarWorking = true;

    [Header("Lights")]
    public Material GreenCross, YellowCross, RedCross;
    public Material GreenCars, YellowCars, RedCars;

    void Start()
    {
        LightsToGreen();
    }

    void Update()
    {
        MoveCars();
        MoveLaneCarIfPassed(Car1);
        MoveLaneCarIfPassed(Car2);
        MoveLaneCarIfPassed(Car3);
        MoveLaneCarIfPassed(Car4);
        MoveLaneCarIfPassed(Car5);
        MoveLaneCarIfPassed(Car6);
        MoveLaneCarIfPassNeg(Car1B);
        MoveLaneCarIfPassNeg(Car2B);
        MoveLaneCarIfPassNeg(Car3B);
        MoveLaneCarIfPassNeg(Car4B);
        MoveLaneCarIfPassNeg(Car5B);
        MoveLaneCarIfPassNeg(Car6B);

        if (TextDialogueDone)
        {
            if (!hasActivatedLightCycle && Mathf.Abs(Car4.transform.position.x - TargetLightsX) < 0.5f)
            {
                if (car4PassCount % 2 == 0 && car4PassCount != 0)
                {
                    CarShouldStop = true;
                    hasActivatedLightCycle = true;
                    VitezaCarPoz = 0f;

                    Invoke(nameof(ToYellow), 1f);
                    Invoke(nameof(ToPedestrianGreen), 2f);
                    Invoke(nameof(ToYellowAgain), 5f);
                    Invoke(nameof(ToGreenCars), 6f);
                }
            }
        }
    }

    void MoveCars()
    {
        if (CarShouldStop) return;
        if (CarWorking == true)
        {
            MoveCarPoz(Car1);
            MoveCarPoz(Car2);
            MoveCarPoz(Car3);
            MoveCarPoz(Car4);
            MoveCarPoz(Car5);
            MoveCarPoz(Car6);
            MoveCarNeg(Car1B);
            MoveCarNeg(Car2B);
            MoveCarNeg(Car3B);
            MoveCarNeg(Car4B);
            MoveCarNeg(Car5B);
            MoveCarNeg(Car6B);
        }
    }

    void MoveCarPoz(GameObject carP)
    {
        carP.transform.position += new Vector3(VitezaCarPoz * Time.deltaTime, 0f, 0f);
    }
    void MoveCarNeg(GameObject carB)
    {
        carB.transform.position += new Vector3(VitezaCarNeg * Time.deltaTime, 0f, 0f);
    }

    void MoveLaneCarIfPassed(GameObject carP)
    {
        if (carP.transform.position.x>= targetXPos)
        {
            carP.transform.position = initialPosition;

            if (carP == Car4)
            {
                car4PassCount++;
                hasActivatedLightCycle = false;
            }
        }
//            carP.transform.position = Final1Position;
//            carP.transform.Rotate(0f, 180f, 0f);
//            carP.transform.position += new Vector3(VitezaCarNeg * Time.deltaTime, 0f, 0f);

//            if (carP == Car4)
//            {
//                hasActivatedLightCycle = false;
//            }
        

//        if(carP.transform.position.x <= targetXNeg)
//        {
//            carP.transform.position = initialPosition;
//            carP.transform.Rotate(0f, 0f, 0f);
//            carP.transform.position += new Vector3(VitezaCarPoz * Time.deltaTime, 0f, 0f);
//
//            if (carP == Car4)
//            {
//                hasActivatedLightCycle = false;
//            }
//        }
    }

    void MoveLaneCarIfPassNeg(GameObject carB)
    {
        if (carB.transform.position.x <= targetXNeg)
        {
            carB.transform.position = Position1;
        }
    }

    void LightsToGreen()
    {
        GreenCross.color = Color.black;
        YellowCross.color = Color.black;
        RedCross.color = Color.red;

        GreenCars.color = Color.green;
        YellowCars.color = Color.black;
        RedCars.color = Color.black;

        VitezaCarPoz = 15f;
        CarShouldStop = false;
    }

    void ToYellow()
    {
        GreenCars.color = Color.black;
        YellowCars.color = Color.yellow;

        RedCross.color = Color.black;
        YellowCross.color = Color.yellow;
    }

    void ToPedestrianGreen()
    {
        YellowCars.color = Color.black;
        YellowCross.color = Color.black;

        RedCars.color = Color.red;
        GreenCross.color = Color.green;
    }

    void ToYellowAgain()
    {
        GreenCross.color = Color.black;
        RedCars.color = Color.black;

        YellowCars.color = Color.yellow;
        YellowCross.color = Color.yellow;
    }

    void ToGreenCars()
    {
        LightsToGreen(); 
    }

    public void EnableLightSequence()
{
    TextDialogueDone = true;
}
}