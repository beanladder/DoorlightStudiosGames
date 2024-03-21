using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    [SerializeField] Camera cam;
    [SerializeField] float screenBorderSize;
    [SerializeField] float cameraSpeed;

    bool isMouseOutsideBorders = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            Vector3 mousePos = GetMouseWorldPosition();
            Vector3 targetPos = (player.position + mousePos) / 2f;

            targetPos.x = Mathf.Clamp(targetPos.x, player.position.x - threshold, player.position.x + threshold);
            targetPos.z = Mathf.Clamp(targetPos.z, player.position.z - threshold, player.position.z + threshold);

            if (isMouseOutsideBorders)
            {
                // Move the camera towards the mouse position outside the screen borders
                Vector3 moveDirection = (mousePos - player.position).normalized;
                targetPos += moveDirection * cameraSpeed * Time.deltaTime;
            }

            // Apply screen borders
            ApplyScreenBorders(ref targetPos);

            transform.position = targetPos;
        }
    }

    void ApplyScreenBorders(ref Vector3 targetPos)
    {
        Vector3 mousePos = GetMouseWorldPosition();

        if (mousePos.x < player.position.x - screenBorderSize ||
            mousePos.x > player.position.x + screenBorderSize ||
            mousePos.z < player.position.z - screenBorderSize ||
            mousePos.z > player.position.z + screenBorderSize)
        {
            isMouseOutsideBorders = true;
        }
        else
        {
            isMouseOutsideBorders = false;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point;
        }

        return Vector3.zero; // Default if the ray doesn't hit anything
    }

    void OnDrawGizmos()
    {
        // Draw screen borders in Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(player.position, new Vector3(screenBorderSize * 2, 0.1f, threshold * 2));
    }
}
