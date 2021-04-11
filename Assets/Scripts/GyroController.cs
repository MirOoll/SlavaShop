using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool isGyroEnebled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    public bool zooming;
    public float zoomSpeed;
    private Camera camera;

    private void Awake()
    {
        camera.GetComponent<Camera>();
    }
    void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
        isGyroEnebled = EnableGyro();
    }

    void Update()
    {
        if (isGyroEnebled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
        if (zooming)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float zoomDistance = zoomSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            camera.transform.Translate(ray.direction * zoomDistance, Space.World);
        }

    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }
}
