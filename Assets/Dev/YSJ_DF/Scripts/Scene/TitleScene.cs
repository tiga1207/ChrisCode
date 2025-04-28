using Scripts.UI;

namespace Scripts.Scene
{
    public class TitleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;

        protected override void InitializeSceneUI()
        {
            UIManager.Instance.LoadSceneUI(SceneType, _sceneUIPrefab);
        }
    }
}