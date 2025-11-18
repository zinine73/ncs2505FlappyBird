using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 상태를 저장할 enum
    public enum State
    {
        TITLE,      // 0 : 0
        READY,      // 1 : 1
        PLAY,       // 2 : 0
        GAMEOVER,   // 3 : 1
        BESTSCORE   // 4 : 0
    }
    public static GameManager Instance;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Animator floorAnim;
    [SerializeField] BirdControl bird;
    [SerializeField] AudioClip acReady;
    [SerializeField] AudioClip acHit;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject[] stateUI;
    [SerializeField] Sprite[] bgSprite;

    State gameState;    // 게임상태를 저장할 변수
    new AudioSource audio;
    public State GameState => gameState;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        // AudioSource 연결
        audio = GetComponent<AudioSource>();
        // 정상적인 게임의 시간이 흐르게
        Time.timeScale = 1.0f;
        // 게임의 시작은 타이틀에서
        GameTitle();
    }
    // 파라미터로 넘어온 clip을 한번 플레이시킨다
    public void PlayAudio(AudioClip clip) => audio.PlayOneShot(clip);
    void ChangeState(State value)
    {
        gameState = value;
        // stateUI에 있는 모든 UI를 끈다
        foreach (var item in stateUI)
        {
            item.SetActive(false);
        }
        // State값을 공통으로 사용하므로 미리 int값으로 변환
        int temp = (int)gameState;
        // 해당하는 Background sprite 연결
        background.sprite = bgSprite[temp % 2];
        // 해당하는 stateUI를 켠다
        stateUI[temp].SetActive(true);
    }
    public void GameTitle() => ChangeState(State.TITLE);
    public void GameReady()
    {
        ChangeState(State.READY);
        // 새 이동
        bird.BirdReady();
        // Ready 소리
        PlayAudio(acReady);
    }
    public void GamePlay()
    {
        ChangeState(State.PLAY);
        bird.OffBirdAnimator();
    }
    public void GameOver()
    {
        ChangeState(State.GAMEOVER);
        // 충돌 소리
        PlayAudio(acHit);
        // 게임 시간을 멈춘다
        //Time.timeScale = 0f;
        // 바닥 애니메이션을 멈춘다
        floorAnim.enabled = false;
        // 베스트스코어를 체크한다
        ScoreManager.Instance.CheckBestScore();
        // restart 버튼은 일단 꺼둔다
        restartButton.SetActive(false);
        // 코루틴을 이용해서 잠시 시간을 지연시킨다
        StartCoroutine(StopTimer());
    }
    IEnumerator StopTimer()
    {
        // 2초 기다렸다 다음 로직 실행
        yield return new WaitForSeconds(2f);
        // 게임 시간을 멈춘다
        Time.timeScale = 0f;
        // restart 버튼 보이게
        restartButton.SetActive(true);
    }
    public void GameBestScore() => ChangeState(State.BESTSCORE);

    // 현재 씬을 다시 불러오기
    public void RestartGame() => SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
}
