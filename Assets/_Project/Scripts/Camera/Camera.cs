using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float height = 10f;
    public float speed = 5f;


    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, height, target.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.fixedDeltaTime);
        }
    }
}
