using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionManager : MonoBehaviour
{
    public static CarSelectionManager instance;
    public CarData selectedCar;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
