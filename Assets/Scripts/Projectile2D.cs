using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] private Transform shootPoint; // จุดที่ยิงออก
    [SerializeField] private GameObject target;    // เป้าเล็ง / Crosshair
    [SerializeField] private Rigidbody2D bulletPrefab;
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
       // if (Input.GetMouseButtonDown(0))
        {
            // ยิง Ray เมื่อคลิกเมาส์ที่ตำแหน่ง screenPos
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin,ray.direction * 5f, Color.red, 5f);

            // รับค่าที่ Ray ชนกับวัตถุ
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            // ถ้า Ray ชนกับ Collider
            if (hit.collider != null)
            {
                // ปรับตำแหน่งเป้าหมาย
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit {hit.collider.name}"); 
            }


            Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position,hit.point,1f);

            Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            shootBullet.linearVelocity = projectileVelocity;
        }
    }
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 direction = target - origin;
        return new Vector2(
            direction.x / time,
            (direction.y / time) + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time);
    }
}
