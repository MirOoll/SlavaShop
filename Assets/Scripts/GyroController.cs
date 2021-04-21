using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    public float mouseSensitivity = 3.0f;

    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    private bool isGyroEnebled;
    private Gyroscope gyro;

    private Quaternion rot;

    public bool zooming;
    public float zoomSpeed;
    private Camera _camera;
    RaycastHit hit;

    private void Awake()
    {

    }
    void Start()
    {
        //cameraContainer = new GameObject("Camera Container");
        //cameraContainer.transform.position = transform.position;
        //transform.SetParent(cameraContainer.transform);
        _camera.GetComponent<Camera>();
        GetComponent<Camera>().orthographic = false;
        isGyroEnebled = EnableGyro();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalRotation = transform.rotation;

    }

    void Update()
    {
//#if UNITY_EDITOR
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
        transform.rotation = originalRotation * xQuaternion * yQuaternion;
//#endif
        if (isGyroEnebled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("поддерживается");
            gyro = Input.gyro;
            gyro.enabled = true;

            //cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        Debug.Log("не поддерживается");
        return false;
    }
}
