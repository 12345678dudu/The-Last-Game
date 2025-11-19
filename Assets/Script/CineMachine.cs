using UnityEngine;

public class CineMachine : MonoBehaviour
{ 
    public float minY = -14f;  // Altura mínima (ex: chão)
    public float maxY = -60f;  // Altura máxima

    void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
        
    }
}

