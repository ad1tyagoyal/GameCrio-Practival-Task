using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private Button m_SpinButton;
    [SerializeField] private Button m_StopButton;

    [SerializeField] private Button m_Reward1Button;
    [SerializeField] private Button m_Reward2Button;
    [SerializeField] private Button m_Reward3Button;
    [SerializeField] private Button m_Reward4Button;
    [SerializeField] private Button m_Reward5Button;
    [SerializeField] private Button m_Reward6Button;
    [SerializeField] private Button m_Reward7Button;
    [SerializeField] private Button m_Reward8Button;
    [SerializeField] private Button m_Reward9Button;


    private void Start() {
        SetOnClickListners();
        SlotMachineManager.instance.Initialize();
        m_StopButton.gameObject.SetActive(false);
    }

    private void SetOnClickListners() {
        m_SpinButton.onClick.RemoveAllListeners();
        m_SpinButton.onClick.AddListener(SpinSlotMachine);
        
        m_StopButton.onClick.RemoveAllListeners();
        m_StopButton.onClick.AddListener(StopSlotMachine);

        m_Reward1Button.onClick.RemoveAllListeners();
        m_Reward1Button.onClick.AddListener(delegate { OnRewardButton(0); });

        m_Reward2Button.onClick.RemoveAllListeners();
        m_Reward2Button.onClick.AddListener(delegate { OnRewardButton(1); });

        m_Reward3Button.onClick.RemoveAllListeners();
        m_Reward3Button.onClick.AddListener(delegate { OnRewardButton(2); });

        m_Reward4Button.onClick.RemoveAllListeners();
        m_Reward4Button.onClick.AddListener(delegate { OnRewardButton(3); });

        m_Reward5Button.onClick.RemoveAllListeners();
        m_Reward5Button.onClick.AddListener(delegate { OnRewardButton(4); });

        m_Reward6Button.onClick.RemoveAllListeners();
        m_Reward6Button.onClick.AddListener(delegate { OnRewardButton(5); });

        m_Reward7Button.onClick.RemoveAllListeners();
        m_Reward7Button.onClick.AddListener(delegate { OnRewardButton(6); });

        m_Reward8Button.onClick.RemoveAllListeners();
        m_Reward8Button.onClick.AddListener(delegate { OnRewardButton(7); });

        m_Reward9Button.onClick.RemoveAllListeners();
        m_Reward9Button.onClick.AddListener(delegate { OnRewardButton(8); });
    }

    private void SpinSlotMachine() {
        SlotMachineManager.instance.RotateReels();
        m_SpinButton.gameObject.SetActive(false);
        m_StopButton.gameObject.SetActive(true);
    }

    private void StopSlotMachine() {
        SlotMachineManager.instance.StopReels();
        m_SpinButton.gameObject.SetActive(true);
        m_StopButton.gameObject.SetActive(false);
    } 

    private void OnRewardButton(int rewardIndex) {
        SlotMachineManager.instance.StopReels(rewardIndex);
        m_SpinButton.gameObject.SetActive(true);
        m_StopButton.gameObject.SetActive(false);
    }


}
