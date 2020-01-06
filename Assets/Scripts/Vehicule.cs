using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vehicule
{
    public string name;
    public List<int> blocks = new List<int>();
    public List<Vector3> positions = new List<Vector3>();
    public List<Vector4> quaternions = new List<Vector4>();
    public List<int> index = new List<int>();
    public List<int> indexJoint = new List<int>();

}
