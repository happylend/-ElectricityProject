using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform start;
    public Transform end;

    public Transform MovePoint;
    // Start is called before the first frame update
    void Start()
    {
        MovePoint.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {


        //设置指示线的起点和终点
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, MovePoint.position);

        MovePoint.position = Vector3.MoveTowards(start.position, end.position, 5 * Time.deltaTime);

    }
}
