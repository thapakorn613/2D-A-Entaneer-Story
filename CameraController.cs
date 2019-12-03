using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform stopPosition;

    public GameObject unityChan;
    public float cameraDistance = 30.0f;

    private float xMax;
    private float xMin;
    private float yMax;
    private float yMin;
    private Camera moveCamera;

    void Awake()
    {
        // 1 
        GetComponent<Camera>().orthographicSize = ((Screen.height / 2 ) / cameraDistance);
        // 2 
        // moveCamera = GetComponent<Camera>();
        // moveCamera.orthographicSize = ((Screen.height/2));
    }

    // LateUpdate() จะเหมือนกับ Update() 
    // ฟังก์ชันนี้ถ้าประกาศพร้อม update() จะเรียกใช้หลัง Update()
    // ส่วนมากจะใช้กับการเปลี่ยนแปลงของตำแหน่งกล้อง ทิศทางของกล้อง โดยให้ทุกอย่างในฉากเปลี่ยนแปลงเสร็จเรียบร้อยก่อน
    // Update() ---> LateUpdate()
    // void FixedUpdate(){

    //     transform.position = new Vector3(target.position.x,target.position.y,target.position.z);
    // }
    void LateUpdate()
    {
        // จอภาพ (--> Viewport ) ด้านขวาสุด และตรงกลาง ถ้าคำนวณเป็นตำแหน่งภายในฉากจะอยู่ตรงไหน 
        // คำนวณหามาเก็บไว้ในตัวแปร right และ center 

        // var up = moveCamera.ViewportToWorldPoint(Vector2.up); 
        // var center = moveCamera.ViewportToWorldPoint(Vector2.one * 0.5f);

        transform.position = new Vector2(target.position.x,target.position.y);
        // transform.position = new Vector2(Mathf.Clamp(target.position.x,xMin,xMax) ,Mathf.Clamp(target.position.y,yMin,yMax));
        // เมื่อตัวละครขยับไปทางขวามากกว่าจุด center ของฉาก
        // ให้กล้องขยับตามตัวละครไปด้วยทางด้านขวา 


        // if (center.x < target.position.x)
        // {
        //     var pos = moveCamera.transform.position;

        //     if (Math.Abs(pos.x - target.position.x) >= 0.0000001f)
        //     {
        //         moveCamera.transform.position = new Vector3(target.position.x,pos.y, pos.z);
        //     }
            
        // }

        // if (center.y < target.position.y) {
        //     var pos = moveCamera.transform.position;
        //     if (Math.Abs(pos.y - target.position.y) >= 0.0000001f)
        //     {
        //         moveCamera.transform.position = new Vector3(pos.x, target.position.y, pos.z);
        //     }
        // }

        // ถ้าตำแหน่งด้านขวามือสุดของฉากขยับไปทางขวาเรื่อยๆ ตามตัวละคร แล้ววิ่งเลยจุด StopPosition
        // ให้เรียกใช้ฟังก์ชัน ClearStage() 
        // แล้วหยุดการทำงานของกล้อง
        // print("target.position.x : " + target.position.x);
        // print("stopPosition.position.x : "+ stopPosition.position.x);

        // if ((stopPosition.position.x - target.position.x) < 0)
        // {            //StartCoroutine(ClearStage());
        //     print("stopPosition.position.x : ");
        //     enabled = false;  // code นี้จะให้ไม่ได้  enabled --> disabled
        // }
        // if (target.position.x < (center.x-10)) {
        //     print("is Dead");

        // }
    }
    private IEnumerator ClearStage()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player) // เช็คก่อนว่าตอนเคลียร์เนี่ยผู้เล่นอยู่ในฉากหรือไม่ ไม่ใช่โดน destroy ไปซะก่อนระหว่างทาง 
        {
			Debug.LogError("clear");
			//player.GetComponent<Demo>().Clear();// เรียกใช้ฟังก์ชัน Clear() ของ Component: Demo
        }

        yield return new WaitForSeconds(3); // รอ 3 วินาทีก่อนที่จะเปลี่ยนกลับเป็นฉาก Start

        SceneManager.LoadScene("End");
    }
}
