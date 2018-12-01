using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SmashpBug : MiniGameManagerBase {

    public MiniGame game = MiniGame.SmashBug;

    public GameObject groundBugPrefab;
    public Transform leftGroundSpawn;
    public Transform rightGroundSpawn;

    private bool won = true;
    private bool gamePlaying;
    private float gameTime;
    private System.DateTime startTime;
    private int bugNum;
    private int bugKilled;
    private int bulletNum;
    private List<BugData> bugs;

	private void OnEnable()
	{
        MiniGameTimerUI.OnTimeUp += OnTimeUp;
        BugUtils.OnBugKilled += OnBugKilled;
        PlayerUtils.OnPlayerDead += OnPlayerDead;
	}

	private void OnDisable()
	{
        MiniGameTimerUI.OnTimeUp -= OnTimeUp;
        BugUtils.OnBugKilled -= OnBugKilled;
        PlayerUtils.OnPlayerDead -= OnPlayerDead;
	}

	private void Start()
	{
        gamePlaying = false;
        LoadGame();
        StartGame();
	}

	public override void StartGame()
    {
        gamePlaying = true;
        startTime = System.DateTime.Now;
        MiniGameTimerUI.Instance.StartTimer();

        bugKilled = 0;
        SpawnBugs();
    }

    public override void EndGame()
    {
        MainGameManager.Instance.FinishMiniGame(game, won);
    }

    public void OnTimeUp()
    {
        //play effect
        if (won && bugKilled < bugNum)
            won = false;
        
        EndGame();
    }

    private void SpawnBugs()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            BugData data = bugs[i];
            StartCoroutine(SpawnBug(bugs[i]));
        }
    }

    IEnumerator SpawnBug(BugData data)
    {
        yield return new WaitForSeconds(data.spawnTime);

        if (!data.flying)
        {
            Transform spawnLocation = data.fromLeft ? leftGroundSpawn : rightGroundSpawn;
            GameObject bug = Instantiate(groundBugPrefab, spawnLocation.position, Quaternion.identity);
            Bug bugCtrl = bug.GetComponent<Bug>();
            bugCtrl.Init(data);
            BossBug bossBugCtrl = bug.GetComponent<BossBug>();
            bossBugCtrl.Init(data.health);
        }

    }

    public void OnBugKilled()
    {
        bugKilled += 1;
    }

    private void OnPlayerDead()
    {
        won = false;
    }

    private void SetupTimer()
    {
        int level = MainGameManager.Instance.GetMiniGameLevel(game);
        float seconds = MainGameManager.Instance.GetMiniGameTime(level);
        MiniGameTimerUI.Instance.ResetTimer(seconds);
    }

    public override void LoadGame()
    {
        int level = MainGameManager.Instance.GetMiniGameLevel(game);
        SetupTimer();

        bugNum = 1;
        bugs = new List<BugData>();

        int health = Mathf.Min(2 + level, 15);
        bugs.Add(new BugData(1.0f, 1, false, false, (Random.value > 0.5f), health));

        ApplyRandomColor();

    }

    private void ApplyRandomColor()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            BugData data = bugs[i];
            data.bugColor = BugColoring.Instance.GetRandomColor();
        }
    }
}
