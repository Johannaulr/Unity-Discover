public class MiniPerf_Script_EventsManager
{
    // ===================================
    //      EVENTS
    // Triggered when a LSL stream connects or disconnects
    public delegate void DebugText(string text);
    public static event DebugText OnLogMessage;

    public static void Send_OnLogMessage(string text)
    {
        if (OnLogMessage != null)
            OnLogMessage(text);
    }
}
