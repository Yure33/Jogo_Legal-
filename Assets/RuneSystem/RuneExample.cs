using UnityEngine;
using System;

[Serializable]
public class RuneExample : IRuneEffect
{
    public GameObject prefabClone;
    
    public void ExecuteEffect(GameObject bolaDeFogo)
    {
        GameObject.Instantiate(prefabClone, bolaDeFogo.transform.position, Quaternion.identity);
    } 
}