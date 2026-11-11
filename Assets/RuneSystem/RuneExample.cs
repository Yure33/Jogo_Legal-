using UnityEngine;
using System;

[Serializable]
public class RuneExample : MonoBehaviour, IRuneEffect
{
    public GameObject prefabClone;
    
    public void ExecuteEffect(BolaFogo_Script bolaDeFogo)
    {
        GameObject.Instantiate(prefabClone, bolaDeFogo.transform.position, Quaternion.identity);
    } 
}