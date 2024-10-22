using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UIManger : MonoBehaviour
{
    public static UIManger Instance;
    [SerializeField] TextMeshProUGUI CoreHp;

    private void Awake()
    {
        Instance = this;
    }
    public void CoreHpUpdate(int hp)
    {
        CoreHp.text = $"Core HP : {hp}";
    }

}
