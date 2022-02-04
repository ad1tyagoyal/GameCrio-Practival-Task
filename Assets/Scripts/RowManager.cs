using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class RowManager : MonoBehaviour {
    //assing in reverwse order to make reel spin upwards
    [SerializeField] private RectTransform[] m_PositionHolders;
    [SerializeField] private GameObject[] m_RowElements;

    private RectTransform[] m_RowElementsTransforms;
    private Image[] m_RowElementsImages;

    private int m_NextSpriteIndex;
    private int[] m_RowElementsPositionIndex;

    private int m_NoOfElements;
    private bool m_IsRotating = false;


    public void Initialize() {
        m_NoOfElements = m_RowElements.Length;
        m_RowElementsPositionIndex = new int[m_NoOfElements];
        m_RowElementsTransforms = new RectTransform[m_NoOfElements];
        m_RowElementsImages = new Image[m_NoOfElements];

        //caching the components for faster accessing
        for (int i = 0; i < m_NoOfElements; i++) {
            m_RowElementsTransforms[i] = m_RowElements[i].GetComponent<RectTransform>();
            m_RowElementsImages[i] = m_RowElements[i].GetComponent<Image>();

            m_RowElementsTransforms[i].position = m_PositionHolders[i].position;
            m_RowElementsImages[i].sprite = SlotMachineManager.instance.GetSpriteAt(i);
            m_RowElementsPositionIndex[i] = i;
        }

        m_NextSpriteIndex = m_NoOfElements-1;
    }

    public void StartRotating() {
        m_IsRotating = true;
        StartCoroutine(ContinueSpinningRow());
    }

    public void StopRotating(int rewardIndex) {
        StartCoroutine(StopSpin(rewardIndex));
    }

    public void StopRotating() {
        float delay = Random.Range(5.0f, 6.0f);
        StartCoroutine(StopSpin(delay));
    }

    private IEnumerator StopSpin(float delay) {
        yield return new WaitForSeconds(delay);
        m_IsRotating = false;
    }


    private IEnumerator StopSpin(int rewardIndex) {
        float delay = 5.0f;
        StartCoroutine(StopSpin(delay));
        yield return new WaitForSeconds(delay);
        m_IsRotating = true;
        //calculating noOfSpins require to get the desired output(selected reward)
        int noOfSpins = 0;
        int totalSprites = SlotMachineManager.instance.GetTotalSpritesCount();
        int selectedIndex = ((m_NextSpriteIndex + 2) + 0) % totalSprites;

        if(selectedIndex > rewardIndex) noOfSpins = selectedIndex - rewardIndex;
        else if(selectedIndex < rewardIndex) noOfSpins = totalSprites - (Mathf.Abs(selectedIndex - rewardIndex));


        StartCoroutine(SpinRow(noOfSpins));
        yield return new WaitForSeconds(noOfSpins * SlotMachineManager.instance.GetTimeToReachNextPosition());
        m_IsRotating = false;
    }

    private IEnumerator ContinueSpinningRow() {
        StartCoroutine(SpinRow(1));
        yield return new WaitForSeconds(SlotMachineManager.instance.GetTimeToReachNextPosition());
        if(m_IsRotating)
            StartCoroutine(ContinueSpinningRow());
    }

    //spins row a specific times
    private IEnumerator SpinRow(int noOfSpins) {
        for (int j = 0; j < noOfSpins; j++) {
            for (int i = 0; i < m_NoOfElements; i++) {
                int nextPositionIndex = (m_RowElementsPositionIndex[i] + 1) % (m_NoOfElements);
                m_RowElementsPositionIndex[i] = nextPositionIndex;

                if (nextPositionIndex == 0) {
                    m_RowElementsImages[i].sprite = SlotMachineManager.instance.GetSpriteAt(m_NextSpriteIndex);
                    m_NextSpriteIndex = (m_NextSpriteIndex + 1) % SlotMachineManager.instance.GetTotalSpritesCount();
                    m_RowElementsImages[i].enabled = false;
                }
                else {
                    m_RowElementsImages[i].enabled = true;
                }

                m_RowElementsTransforms[i].DOMoveY(m_PositionHolders[nextPositionIndex].position.y,
                                                    SlotMachineManager.instance.GetTimeToReachNextPosition(), false);
            }
        }

        yield return new WaitForSeconds(0);
    }
    
}
