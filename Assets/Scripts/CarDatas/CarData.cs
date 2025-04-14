using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarData", menuName = "Car/CarData")]
public class CarData : ScriptableObject
{
    public string carName;
    public Sprite previewSprite;
    public int speed;
    public int handling;
    public int health;
    public int healthUpgradeLevel = 0;
}
