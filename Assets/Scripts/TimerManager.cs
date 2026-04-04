using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TimerManager : MonoBehaviour {
	private static List<Timer> timers = new List<Timer>(); 
	private static Stack<Timer> removedTimers = new Stack<Timer> (); 
	public static bool isPlaying = true; 
    private static GameObject instance;

    private void Start() {
        if (instance == null)
            TimerManager.instance = this.gameObject;
    }
    private void FixedUpdate() {
        foreach (Timer t in timers.ToList<Timer>())
            t.Update(Time.deltaTime);

        while (removedTimers.Count != 0 ) {
            Timer t = removedTimers.Pop();
            timers.Remove(t);
        }
    }

    private static void CreateInstance() {
        instance = new GameObject("Timer Manager");
        instance.AddComponent<TimerManager>();
        DontDestroyOnLoad(instance);
    }

	public static void SetupTimer(Timer t){
        if (instance == null) CreateInstance();
		timers.Add (t);
	}

    public static void RemoveTimer (Timer t) {
        removedTimers.Push(t);
    }
}