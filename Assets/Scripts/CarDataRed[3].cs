using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarData", menuName = "Car/CarData")]
public class CarData : ScriptableObject
{
    public string carName;
    public Sprite carSprite;
    public int maxHealth = 3;
    // Add other stats like speed, handling, etc. here
}
