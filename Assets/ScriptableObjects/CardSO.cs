using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/CardSO")]
public class CardSO : ScriptableObject {
    public string title;
    public string description;
    public Sprite image;
}
