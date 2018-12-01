using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCountManager : SingletonBehaviour<BugCountManager> {

    public GameObject bugCountObj;
    public TMPro.TextMeshPro bugCountText;

    private PulseAnimation pulseCtrl;
    private int count = 0;

	private void Start()
	{
        pulseCtrl = bugCountObj.GetComponent<PulseAnimation>();
	}

	private void OnEnable()
    {
        BugUtils.OnBugKilled += UpdateCount;
        BugUtils.OnBugPainted += UpdateCount;
    }

    private void OnDisable()
    {
        BugUtils.OnBugKilled -= UpdateCount;
        BugUtils.OnBugPainted -= UpdateCount;
    }

    private void UpdateCount()
    {
        count++;
        bugCountText.text = count.ToString();
        pulseCtrl.Play();
    }


}
