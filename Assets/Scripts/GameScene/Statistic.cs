using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour {

    private Statistic() { }
    public static Statistic Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private readonly static float resetComboTime = 4f;

    private bool timing;
    //private float gameTime;
    public float GameTimer { get; private set; }

    public int BeHit { get; private set; }
    public int Kills { get; private set; }

    public int Combo { get; private set; }
    private float comboTimer;
    public int MaxCombo { get; private set; }

    // Use this for initialization
    void Start () {
        timing = false;
        ResetAllTimer();
	}
    private void FixedUpdate()
    {
        if(comboTimer>0)
        {
            comboTimer -= Time.unscaledDeltaTime;
            if (comboTimer <= 0)
            {
                //MaxCombo = Combo > MaxCombo ? Combo : MaxCombo;
                Combo = 0;
                //Debug.Log("resetCombo");
            }
        }
        if(timing)
            GameTimer += Time.unscaledDeltaTime;
    }

    public void KillsAcc()
    {
        Kills++;
    }
    public void BeHitAcc()
    {
        BeHit++;
    }
    public void ResetBehit()
    {
        BeHit = 0;
    }
    public void ComboAcc()
    {
        Combo++;
        MaxCombo = Combo>MaxCombo?Combo:MaxCombo;
        comboTimer = resetComboTime;
        //Debug.Log("AccCombo");
    }

    public void StartTiming()
    {
        timing = true;
    }
    public void EndTiming()
    {
        timing = false;
    }
    public void ResetAllTimer()
    {
        GameTimer = 0;
        MaxCombo = 0;
        Combo = 0;
        BeHit = 0;
        Kills = 0;
    }
    //IEnumerable ResetComco()
    //{
    //    yield return new WaitForSecondsRealtime(resetComboTime);
    //    if (combo > maxCombo)
    //        maxCombo = combo;
    //    combo = 0;
    //}
}
