using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    Camera cam;
    GameObject player;
    public float xMargin = 0.5f;
    public float yMargin = 2f;
    public float xSmooth = 4f;
    public float ySmooth = 1f;
    public float center = -2f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        TrackPlayer();
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.transform.position.y + center) > yMargin;
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;
        if (CheckXMargin())
            targetX = Mathf.Lerp(transform.position.x, player.transform.position.x, xSmooth * Time.deltaTime);
        if (CheckYMargin())
            targetY = Mathf.Lerp(transform.position.y, player.transform.position.y, ySmooth * Time.deltaTime);
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
