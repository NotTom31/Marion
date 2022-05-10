using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossPointer : MonoBehaviour
{
    [SerializeField]private Camera uiCamera;
    public Image pointerImage;
    private GameObject boss;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    private void Start()
    {
        boss = GameObject.Find("RatPlagueDrBoss");
        targetPosition = boss.transform.position;
        pointerRectTransform = this.GetComponent<RectTransform>();
        pointerImage = this.GetComponent<Image>();
    }
    private void Update()
    {
        uiCamera.targetDisplay = 0;
        targetPosition = boss.transform.position;
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        if(isOffScreen)
        {
            pointerImage.enabled = true;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= 0) cappedTargetScreenPosition.x = 0f;
            if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width;
            if (cappedTargetScreenPosition.y <= 0) cappedTargetScreenPosition.y = 0f;
            if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            pointerImage.enabled = false;
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
    
    }
}
