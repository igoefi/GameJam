public abstract class CutsceneStart
{
    public static void Start(string cutsceneName) =>
        CutsceneManager.Instance.StartCutscene(cutsceneName);

}
