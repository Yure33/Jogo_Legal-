using System;
using UnityEngine;

public class SupportEntity : MonoBehaviour
{
    public event Action OnSupportDestroyed;

    void OnDestroy()
    {
        // Quando o Obstacle.cs der um Destroy() no balão, isso avisa a TNT automaticamente
        OnSupportDestroyed?.Invoke();
    }
}