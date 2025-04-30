using Scripts.Manager;

namespace Scripts.Scene
{
    public class InGameScene : SceneBase
    {
        public override SceneID SceneID => SceneID.InGame;

        public override void Initialize()
        {
            base.Initialize();
            ManagerGroup.Instance.RegisterManager(InGameManager.Instance);
            ManagerGroup.Instance.RegisterManager(AudioManager.Instance);
        }

        private void Start()
        {
        }
    }
}