using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    PlayController ac;
    public IUserInput pi;
    public float horizontalSpeed=100f;
    public float verticalSpeed = 50f;
    public Image lockDot;//中点
    public bool lockState;
    public bool isAI = false;


    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;//取得模型物件
    private GameObject camera;

    private Vector3 cameraDampVelocity;//
    [SerializeField]
    private LockTarget lockTarget;

    // Start is called before the first frame update
    void Start()
    {
        
        
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        //Debug.Log(cameraHandle);
        ac = playerHandle.GetComponent<PlayController>();//取得PlayController
        pi = ac.pi;

        tempEulerX = 20f;
        
        model = ac.model;
        //model = playerHandle.GetComponent<PlayController>().model;
        if (isAI == false)
        {
            camera = Camera.main.gameObject;
            lockDot.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;//锁定鼠标
        }
        
        lockState = false;

       

    }

    void Update()
    {
        if (lockTarget != null)
        {
            if (!isAI)
            {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0,lockTarget.halfHeight,0));//取得锁定目标的半高坐标并转化为像素坐标，然后使UI点图片的坐标获得它
            }

            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)//与目标距离过远时接触锁定
            {
                LockProcessA(null, false, false, isAI);
                //lockTarget = null;
                //lockDot.enabled = false;
                //lockState = false;
            }
        }
    }

    private void LockProcessA(LockTarget _lockTarget,bool _lockDotEnabled,bool _lockState,bool _isAI)
    {
        lockTarget = _lockTarget;
        if (_isAI == false)
        {
            lockDot.enabled = _lockDotEnabled;
        }

        lockState = _lockState;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;//获得model的eulerAngles角
            playerHandle.transform.Rotate(Vector3.up, pi.JRight * horizontalSpeed * Time.fixedDeltaTime);
            tempEulerX -= pi.JUp * verticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -30, 35);//将x角限制在-30到35之间
                                                          //cameraHandle.transform.eulerAngles = new Vector3(tempEulerX, cameraHandle.transform.eulerAngles.y, cameraHandle.transform.eulerAngles.z);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);//x角进行变化

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {

            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;//取得玩家到敌人间的向量
            tempForward.y = 0;//去除y轴的向量，使其变成表示朝向的向量
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform.position);
        }



        //camera.transform.position = transform.position;

        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position,0.2f);//使摄影机的坐标追上上本物体的坐标
        //camera.transform.eulerAngles = transform.eulerAngles;//使摄影机的eulerAngles追上本物体的
        if (isAI == false)
        {
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, 0.2f);
        camera.transform.LookAt(cameraHandle.transform);
        }


        //lockDot.transform.position = Camera.main.WorldToScreenPoint(lockTarget.transform.position);//ui跟随锁定物体
    }

    public void LockUnlock()
    {
        //try to lock
        Vector3 modelOrigin1 = model.transform.position;//玩家坐标
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);//玩家视线坐标
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f),model.transform.rotation,LayerMask.GetMask(isAI?"Player":"Enemy"));//判断并使取得玩家取得面前的敌人，使敌人取得玩家
        //Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if (cols.Length==0)
        {
            LockProcessA(null, false, false, isAI);
            //lockTarget = null;
            //lockDot.enabled = false;
            //lockState = false;
        }
        else
        {
            //Collider a=cols[0];
            foreach (var col in cols)
            {
                if (lockTarget != null && col.gameObject == lockTarget.obj)
                {
                    LockProcessA(null, false, false, isAI);
                    break;
                }
                //if (a.transform.position.z > (col.transform.position.z - modelOrigin1.z))
                //    {
                //    a = col;
                //    }
                //    lockTarget = a.gameObject;

                //lockTarget = new LockTarget(col.gameObject, col.bounds.extents.y);//取得物体及其半高
                LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                
                //lockDot.enabled = true;
                //lockState = true;

                break;

            }
        }

        
            

            //Collider col=cols[0];
            //for (int i = 0;1 < cols.Length; i++)
            //{
            //    if (col > (cols[i + 1].transform.position.z - modelOrigin1.z))
            //    {
            //        col = cols[i];
            //    }

            //}
            //lockTarget = col.gameObject;
        //}
        //else
        //{
        //    lockTarget = null;
        //}
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;

        public LockTarget(GameObject _obj, float _halfHeight)//获得变量
        {
            obj = _obj;
            halfHeight = _halfHeight;
        }
    }

}
