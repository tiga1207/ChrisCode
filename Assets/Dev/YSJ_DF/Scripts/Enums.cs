namespace Scripts
{
    public enum SceneType
    {
        None = 0,
        Title,
        InGame,
    }

    // 결과 확인
    public enum GameResultState
    {
        None,   // 아직 결과 없음
        Clear,  // 클리어 성공
        Fail    // 클리어 실패
    }

    // 게임 상태용
    public enum GamePlayState
    {
        None,     // 기본 상태 (선택사항)
        Playing,  // 게임이 진행 중
        Paused,   // 일시 정지
        Stopped   // 강제 멈춤 (게임 결과)
    }
}