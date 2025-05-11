using UnityEngine;

public class CarSystem : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Car1, Car2, Car3, Car4, Car5, Car6;

    [Header("Essentials")]
    public float VitezaCar = 15f;
    public float targetX = 100f;
    private Vector3 initialPosition = new Vector3(-100f, 1.5f, -2.5f);
    public float TargetLightsX = -15f;

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
        ResetCarIfPassed(Car1);
        ResetCarIfPassed(Car2);
        ResetCarIfPassed(Car3);
        ResetCarIfPassed(Car4);
        ResetCarIfPassed(Car5);
        ResetCarIfPassed(Car6);

        if (!hasActivatedLightCycle && Mathf.Abs(Car4.transform.position.x - TargetLightsX) < 0.5f)
        {
            CarShouldStop = true;
            hasActivatedLightCycle = true;
            VitezaCar = 0f;

            Invoke(nameof(ToYellow), 1f);          
            Invoke(nameof(ToPedestrianGreen), 2f); 
            Invoke(nameof(ToYellowAgain), 5f);     
            Invoke(nameof(ToGreenCars), 6f);       
        }
    }

    void MoveCars()
    {
        if (CarShouldStop) return;
        if(CarWorking == true)
        {
        MoveCar(Car1);
        MoveCar(Car2);
        MoveCar(Car3);
        MoveCar(Car4);
        MoveCar(Car5);
        MoveCar(Car6);
        }
    }

    void MoveCar(GameObject car)
    {
        car.transform.position += new Vector3(VitezaCar * Time.deltaTime, 0f, 0f);
    }

    void ResetCarIfPassed(GameObject car)
    {
        if (car.transform.position.x >= targetX)
        {
            car.transform.position = initialPosition;

            if (car == Car4)
            {
                hasActivatedLightCycle = false;
            }
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

        VitezaCar = 15f;
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
}