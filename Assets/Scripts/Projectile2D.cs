using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] private Transform shootPoint; // จุดที่ยิงออก
    [SerializeField] private GameObject target;    // เป้าเล็ง / Crosshair
    [SerializeField] private GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // อ่านค่าตำแหน่งเมาส์ และเก็บค่า screenPos
        Vector2 screenPos = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // ยิง Ray เมื่อคลิกเมาส์ที่ตำแหน่ง screenPos
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

            // รับค่าที่ Ray ชนกับวัตถุ
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            // ถ้า Ray ชนกับ Collider
            if (hit.collider != null)
            {
                // ปรับตำแหน่งเป้าหมาย
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit {hit.collider.gameObject.name}"); // ปริ้นชื่อวัตถุที่ชน
            }
        }
    }
}
