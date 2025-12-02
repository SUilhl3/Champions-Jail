using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 5f;
    public Vector3 mapMin, mapMax;

    Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = new Vector3(player.position.x, player.position.y, player.position.z);
        float vertLim = camera.orthographicSize;
        float horiLim = vertLim * camera.aspect;

        nextPos.x = Mathf.Clamp(nextPos.x,mapMin.x + horiLim,mapMax.x - horiLim);
        nextPos.y = Mathf.Clamp(nextPos.y, mapMin.y + vertLim, mapMax.y - vertLim);

        transform.position = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * cameraSpeed);
    }
}
