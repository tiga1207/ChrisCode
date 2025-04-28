namespace Scripts.Scene
{
    public class InGameScene : SceneBase
    {
        public override SceneType SceneType => SceneType.InGame;

        protected override void InitializeSceneUI()
        {
            // InGame 씬 전용 UI 세팅
        }
    }
}