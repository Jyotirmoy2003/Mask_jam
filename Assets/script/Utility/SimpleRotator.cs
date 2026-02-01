using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Y axis by default
    public float rotationSpeed = 60f;         // Degrees per second
    public bool useLocalRotation = true;
    [SerializeField] Transform target;

    private void Update()
    {
        Vector3 rotation = rotationAxis.normalized * rotationSpeed * Time.deltaTime;

        if (useLocalRotation)
        {
            target.Rotate(rotation, Space.Self);
        }
        else
        {
            target.Rotate(rotation, Space.World);
        }
    }
}
