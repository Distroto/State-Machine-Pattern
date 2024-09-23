using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}