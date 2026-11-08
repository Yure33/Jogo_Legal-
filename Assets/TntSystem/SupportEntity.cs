using System;
using UnityEngine;

public class SupportEntity : MonoBehaviour
{
    [SerializeField] private TntFall tnt;

    void OnDestroy()
    {
        if(tnt != null)
        {
            tnt.Fall();
        }
    }
}