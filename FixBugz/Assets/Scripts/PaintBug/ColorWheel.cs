using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ColorWheel : SingletonBehaviour<ColorWheel>
{
    public GameObject m_SpinWheel;
    public float anglePerNumber;
    public int totalNumber;
    public int wheelNumber = 1;

    public List<BugColor> bugColors;


    private string id;
    private float wheelSpinProgress = 0.0f;
    private float wheelSpinTime = 0.2f;
    private bool wheelSpinning = false;
    private float spinFrom = 0;
    private float spinTo = 0;
    private float spinSpan = 0;
    private float spinSpeed = 10;

    private float currentAngle;
    private float targetAnble;

    private void Start()
    {
        id = "color_spin";
        TryUpdateWheel(2);
    }

    void Update()
    {
        /*
        if (wheelSpinning)
        {
            currentAngle = spinFrom + spinSpan * wheelSpinProgress;
            m_SpinWheel.transform.eulerAngles = new Vector3(0, 0, currentAngle);
        }
        */

        float offset = 0;
        if (currentAngle < targetAnble)
        {
            offset = Mathf.Min(spinSpeed, targetAnble - currentAngle);
        }
        else if (currentAngle > targetAnble)
        {
            offset = -Mathf.Min(spinSpeed, currentAngle - targetAnble);
        }

        currentAngle += offset;
        m_SpinWheel.transform.eulerAngles = new Vector3(0, 0, currentAngle);
    }

    public void TrySpinLeft()
    {
        if (wheelNumber > 1 )
        {
            TryUpdateWheel(wheelNumber - 1);
        }
    }

    public void TrySpinRight()
    {
        if (wheelNumber < totalNumber)
        {
            TryUpdateWheel(wheelNumber + 1);
        }
    }


    private void TryUpdateWheel(int spinToNumber)
    {
        if (wheelNumber == spinToNumber)
            return;

        wheelNumber = spinToNumber;
        targetAnble = anglePerNumber * (wheelNumber - 1);
    }

    /*
    private void SpinWheel(int spinFromNumber, int spinToNumber)
    {
        spinFrom = currentAngle;
        spinTo = anglePerNumber * (spinToNumber - 1);
        spinSpan = spinTo - spinFrom;
        wheelSpinProgress = 0;
        wheelSpinning = true;
        DOTween.To(() => wheelSpinProgress, x => wheelSpinProgress = x, 1, wheelSpinTime).SetEase(Ease.InOutBack).OnComplete(OnWheelSpinFinish);

        wheelNumber = spinToNumber;
    }
    */

    private void OnWheelSpinFinish()
    {
        wheelSpinning = false;
    }


    //todo: doesn't belone here
    public BugColor GetCurrentColor()
    {
        return bugColors[wheelNumber - 1];
    }
}
