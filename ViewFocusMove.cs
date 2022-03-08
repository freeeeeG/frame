using UnityEngine;
using Cinemachine;
public class ViewFocusMove : MonoBehaviour
{
    Vector3 startPos;
    float distanceX;//相机跟踪点与角色的X轴距离
    float maxDistanceX = 3f;//相机跟踪点与角色的最大X轴距离
    float distanceY;//相机跟踪点与角色的Y轴距离
    Transform player;
    Transform pointL;
    Transform pointR;
    private float longPressTime;
    private bool isLongPress;
    private bool isVerticalView;
    public CinemachineVirtualCamera cv;
    // Start is called before the first frame update
    private void Awake()
    {
        isLongPress = false;
        longPressTime = 0f;
    }
    void Start()
    {
        player = GameObject.Find("Player").transform;
        pointL = GameObject.Find("FocusPointL").transform;
        pointR = GameObject.Find("FocusPointR").transform;
        cv = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();

        CVFollowViewFocus();
    }

    // Update is called once per frame
    void Update()
    {

        startPos = player.position;


        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //CVFollowViewFocus();
            isVerticalView = false;
            // if (Input.GetAxisRaw("Horizontal") > 0)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, pointR.localPosition, 10f*(5-4*Input.GetAxis("Horizontal")) * Time.deltaTime);

            //Debug.Log(pointR.localPosition);
            // if (Input.GetAxisRaw("Horizontal") < 0)
            // {
            //     transform.localPosition = Vector3.MoveTowards(transform.localPosition, pointL.localPosition, 10f*(5+4*Input.GetAxis("Horizontal")) * Time.deltaTime);
            // }
            
            

        }
        else
        {
            //CVFollowViewFocus();
            LongPress(Input.GetButton("Vertical"));
            if (isLongPress)
            {
                longPressTime += Time.deltaTime;
                if (longPressTime >= 0.5f && !isVerticalView)
                {
                    distanceY = Input.GetAxis("Vertical")*6f;

                    transform.position = transform.position + new Vector3(0, distanceY, 0);
                    isVerticalView = true;
                }
            }
            else
            {
                if (isVerticalView)
                {
                    transform.position = transform.position - new Vector3(0, distanceY, 0);
                    distanceY = 0;
                    isVerticalView = false;
                }
            }
        }


    }
    public void LongPress(bool press)
    {
        isLongPress = press;
        if (press == false)
        {
            longPressTime = 0f;
        }
    }
    public void CVFollowPlayer()
    {
        cv.Follow = player;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0f;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1;
    }
    public void CVFollowViewFocus()
    {
        cv.Follow = transform;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0f;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 3;
        cv.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 3;
    }
    private void FixedUpdate()
    {
        
    }
}
