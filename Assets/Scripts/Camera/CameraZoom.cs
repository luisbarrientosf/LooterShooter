using UnityEngine;
using Unity.Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    // Zoom limits
    public float zoomSpeed = 20f;
    public float minZoom = 6f;
    public float maxZoom = 12f;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0 && cinemachineCamera != null)
        {
            var lens = cinemachineCamera.Lens;

            if (!lens.Orthographic)
            {
                lens.FieldOfView -= scrollInput * zoomSpeed;
                lens.FieldOfView = Mathf.Clamp(lens.FieldOfView, minZoom, maxZoom);
            }
            else
            {
                lens.OrthographicSize -= scrollInput * zoomSpeed;
                lens.OrthographicSize = Mathf.Clamp(lens.OrthographicSize, minZoom, maxZoom);
            }

            cinemachineCamera.Lens = lens; // Apply changes
        }
    }
}