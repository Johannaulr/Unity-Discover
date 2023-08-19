using UnityEngine;
using TMPro;

public class MiniPerf_Script_DebugCanvasController : MonoBehaviour
{
    public TMP_Text textObjectUI;

    private void OnEnable()
    {
        MiniPerf_Script_EventsManager.OnLogMessage += AddDebugText;
    }

    private void OnDisable()
    {
        MiniPerf_Script_EventsManager.OnLogMessage -= AddDebugText;
    }

    public void AddDebugText(string text)
    {
        textObjectUI.text += "\n" + text;
    }

    public void ClearDebugText()
    {
        textObjectUI.text = "...";
    }

    /*
    public void TestRPC_PlaySound()
    {
        MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.PlayAudio);
    }

    public void TestRPC_StopSound()
    {
        MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.StopAudio);
    }

    public void TestRPC_DarkPassThru()
    {
        MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.DarkPassThru);
    }

    public void TestRPC_NormalPassThru()
    {
        MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.NormalPassThru);
    }
    */
}
