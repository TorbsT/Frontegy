using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] Vector2 fovLimits;
    [SerializeField] Vector2 osLimits;
    [SerializeField] Vector2 horizontalAngleLimits;
    [SerializeField] Vector2 verticalAngleLimits;
    [SerializeField] float height;
    [SerializeField] bool orthographic = false;
    [SerializeField] float cameraPositionSpeed = 1f;
    [SerializeField] float cameraRotationSpeed = 1f;
    [SerializeField] float cameraFovSpeed = 1f;


    [Range(30f, 90f)]
    [SerializeField] float verticalAngle;
    [Range(0f, 359f)]
    [SerializeField] float horizontalAngle;
    float fov;
    Camera camera;
    float circleRadius;
    float zoomSpeed;
    bool enableCameraMovement = true;
    bool isInitialized = false;
    
    void ManualStart()
    {
        isInitialized = true;
        camera = GetComponent<Camera>();
    }
    public void freeView()
    {
        if (!isInitialized) ManualStart();
        if (orthographic)
        {
            enableCameraMovement = true;
            camera.orthographic = true;
        }
        else if (!orthographic)
        {
            enableCameraMovement = true;
            camera.orthographic = false;
        }
        if (enableCameraMovement)
        {
            horizontalAngle += Time.deltaTime * -Input.GetAxis("Horizontal") * 200f;
            if (horizontalAngle < 0f) horizontalAngle += 360f;
            else if (horizontalAngle >= 360f) horizontalAngle -= 360f;
            verticalAngle += Time.deltaTime * Input.GetAxis("Vertical") * 20f;
            verticalAngle = Mathf.Clamp(verticalAngle, verticalAngleLimits[0], verticalAngleLimits[1]);

            circleRadius = AdvancedMafs(verticalAngle, transform.position.y);

            Quaternion newRotation = Quaternion.Euler(new Vector3(verticalAngle, horizontalAngle, 0f));
            Vector2 periferalVector = GetPeriferalVector(horizontalAngle, circleRadius);
            Vector3 newPosition = new Vector3(periferalVector[0], height, periferalVector[1]);

            camera.fieldOfView = Maffs.FloatLerp(camera.fieldOfView, GetFov(), cameraFovSpeed);
            camera.orthographicSize = Maffs.FloatLerp(camera.orthographicSize, GetOs(), cameraFovSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, cameraRotationSpeed);
            transform.position = Vector3.Lerp(transform.position, newPosition, cameraPositionSpeed);
        }

        if (Input.GetKeyDown("o")) orthographic = !orthographic;
    }
    public void eagleView()
    {
        if (!isInitialized) ManualStart();
        Vector3 eulerAngles = new Vector3(90f, 0f, 0f);
        Quaternion rotation = Quaternion.identity*Quaternion.Euler(eulerAngles);
        transform.rotation = rotation;
        transform.position = new Vector3(0f, height, 0f);
    }
    float GetFov()
    {
        float x = fovLimits[0] + GetRelativeAngle()*(fovLimits[1] - fovLimits[0]);
        return x;
    }
    float GetOs()
    {
        float x = osLimits[0] + GetRelativeAngle() * (osLimits[1] - osLimits[0]);
        return x;
    }
    float GetRelativeAngle()
    {
        return (verticalAngle - verticalAngleLimits[0]) / (verticalAngleLimits[1] - verticalAngleLimits[0]);
    }
    Vector2 GetPeriferalVector(float degs, float mag)
    {
        float rads = GetRadians(degs);
        Vector2 a = Vector2.up; // a: base vector to rotate degs degrees
        float x = a[0];
        float y = a[1];
        Vector2 returnVector = new Vector2(x * Mathf.Cos(rads) + y * Mathf.Sin(rads), -x * Mathf.Sin(rads) + y * Mathf.Cos(rads));
        returnVector *= mag;
        return returnVector;
    }
    float AdvancedMafs(float degs, float h)
    {
        float x;
        x= Mathf.Tan(GetRadians(degs+90)) * h;
        return x;
    }
    float GetRadians(float degs)
    {
        return (degs * Mathf.PI / 180f);
    }
}
