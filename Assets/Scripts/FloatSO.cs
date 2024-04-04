using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This creates a scriptable object that allows us to save our score
[CreateAssetMenu(menuName = "Scriptable Objects/Float SO")]
public class FloatSO : ScriptableObject
{
    public float value;

}