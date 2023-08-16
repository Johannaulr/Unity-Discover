using UnityEngine;
using TMPro;

public class DebugCanvasController : MonoBehaviour
{
    public TMP_Text textObjectUI;

    private void OnEnable()
    {
        OperaEventsManager.OnLogMessage += AddDebugText;
    }

    private void OnDisable()
    {
        OperaEventsManager.OnLogMessage -= AddDebugText;
    }

    public void AddDebugText(string text)
    {
        textObjectUI.text += "\n" + text;
    }

    public void ClearDebugText()
    {
        textObjectUI.text = "";
    }


    public void TestRPC_PlaySound()
    {
        OperaGameManager.instance.SendRPC(OperaTypesRPC.PlayAudio);
    }

    public void TestRPC_StopSound()
    {
        OperaGameManager.instance.SendRPC(OperaTypesRPC.StopAudio);
    }

    public void TestRPC_SpawnCube()
    {
        OperaGameManager.instance.SendRPC(OperaTypesRPC.SpawnCube);
    }
}
