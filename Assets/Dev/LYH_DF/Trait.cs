using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Trait : MonoBehaviour
{
    public string traitName;
    public string description;
    public TraitType type;
    public float value;
}
