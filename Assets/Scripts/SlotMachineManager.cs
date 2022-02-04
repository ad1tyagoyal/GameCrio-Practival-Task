using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour {
    [SerializeField] private float m_TimeToReachNextPosition;
    [SerializeField] private Sprite[] m_Sprites;
    [SerializeField] private RowManager[] m_RowManagers;

    [HideInInspector] public static SlotMachineManager instance;

    private void Awake() {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
    }


    public void Initialize() {
        for (int i = 0; i < m_RowManagers.Length; i++) {
            m_RowManagers[i].Initialize();
        }
    }

    public void RotateReels() {
        StartCoroutine(StartRotatingReels());
    }

    public void StopReels(int rewardIndex) {
        for (int i = 0; i < m_RowManagers.Length; i++) {
            m_RowManagers[i].StopRotating(rewardIndex);
        }
    }
    
    public void StopReels() {
        for (int i = 0; i < m_RowManagers.Length; i++) {
            m_RowManagers[i].StopRotating();
        }
    }


    public Sprite GetSpriteAt(int index) {
        if(index < m_Sprites.Length) return m_Sprites[index];
        else return null;
    }

    public int GetTotalSpritesCount() {
        return m_Sprites.Length;
    }

    public float GetTimeToReachNextPosition() {
        return m_TimeToReachNextPosition;
    }

    private IEnumerator StartRotatingReels() {
        for (int i = 0; i < m_RowManagers.Length; i++) {
            m_RowManagers[i].StartRotating();
            yield return new WaitForSeconds(Random.Range(0, 1.0f));
        }
    }

}
