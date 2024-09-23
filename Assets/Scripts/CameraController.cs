using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; 
    [SerializeField] private float fastMoveMultiplier = 2f; 

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
