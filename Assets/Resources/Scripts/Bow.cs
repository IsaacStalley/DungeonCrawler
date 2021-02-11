using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public bool shooting;
    private float drawTimer = 0f;
    private float bowTime = 1f;
    private float bowSpeed = 20f;
    public GameObject attachedArrow;
    public GameObject arrow;
    public GameObject frontArm;
    public Transform frontArmTransform;
    public GameObject backArm;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
    attachedArrow = GameObject.Find("Arrow");
    arrow = Resources.Load<GameObject>("Prefabs/Arrow");
    frontArm = GameObject.Find("FrontArm");
    frontArmTransform = GameObject.Find("FrontArm").transform;
    backArm = GameObject.Find("BackArm");
    weapon = GameObject.Find("Weapon");
}

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        ShootBowCheck();
    }

    void ShootBowCheck()
    {
        if (Input.GetMouseButton(0))
        {
            shooting = true;
            attachedArrow.GetComponent<SpriteRenderer>().sprite = arrow.GetComponent<SpriteRenderer>().sprite;
            FollowMouse();
            AddDrawTime();
        }
        CheckIfReleasedBow();
    }

    private void AddDrawTime()
    {
        drawTimer += Time.deltaTime;
    }
    private void CheckIfReleasedBow()
    {
        if (Input.GetMouseButtonUp(0) && shooting == true)
        {
            FireBow();
        }
    }

    void FireBow()
    {
        InstantiateArrow();
        drawTimer = 0f;
        shooting = false;
        weapon.GetComponent<SpriteRenderer>().sortingOrder = 1;
        attachedArrow.GetComponent<SpriteRenderer>().sprite = null;
    }

    private float SpeedCalc()
    {
        var tempTimer = drawTimer;
        if (drawTimer >= bowTime)
        {
            tempTimer = bowTime;
        }
        var time = tempTimer / bowTime * 100f;
        var speed = time * bowSpeed / 100f;
        if (speed < 10) { speed = 10; }
        return speed;
    }

    private void InstantiateArrow()
    {
        Vector3 mousePos = MousePosition();
        var angle = AngleBackArm(frontArmTransform.position, mousePos) - 90;
        Vector3 force = mousePos - frontArmTransform.position;
        GameObject clone = Instantiate(arrow, frontArmTransform.position + force.normalized, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        clone.SetActive(true);
        clone.name = arrow.name;
        var rigid = clone.GetComponent<Rigidbody2D>();
        rigid.AddForce(force.normalized * SpeedCalc(), ForceMode2D.Impulse);
    }

    private void FollowMouse()
    {
        Vector3 mousePos = MousePosition();
        var Fangle = AngleBackArm(frontArmTransform.position, mousePos) * (150 / 90) - 90;
        var Bangle = AngleBackArm(frontArmTransform.position, mousePos) - 90;

        frontArm.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Fangle));
        backArm.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Bangle));

        weapon.GetComponent<SpriteRenderer>().sortingOrder = 4;
    }

    private float AngleBackArm(Vector3 a, Vector3 b)
    {
        var angle = Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        if (GetComponent<Player>().facingRight)
        {
            if (angle > -90 && angle < 0)
                return -90;
            else if (angle < 90 && angle > 0)
                return 90;
            else
                return angle;
        }
        else
        {
            if (angle > 90)
                return 90;
            else if (angle < -90)
                return -90;
            else
                return angle;
        }
    }
    public Vector3 MousePosition()
    {
        var x = Input.mousePosition.x;
        var y = Input.mousePosition.y;
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
        position[2] = 0;
        return position;
    }
}
