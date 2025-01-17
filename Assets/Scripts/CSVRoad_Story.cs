using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CSVRoad_Story : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogue;

    [Header("스토리 진행도")]
    private List<Dictionary<string, object>> data; // CSV 파일 저장용
    private int progress = 0;    // 현재 진행도

    [Header("선택지")]
    [SerializeField] GameObject dialogue_Box;
    [SerializeField] GameObject dialogue_Obj;
    [SerializeField] TextMeshProUGUI select_1;
    [SerializeField] TextMeshProUGUI select_2;
    public int dialogue_Cnt = 0;  // 플레이어/npc/선택지 위치
    public string dialogue_State; // 플레이어 /npc/ 선택지 판별

    void Start()
    {
        // CSV 파일 읽기
        data = CSVReader.Read("Story");

        if (data != null && data.Count > 0)
        {
            Debug.Log($"CSV 데이터 로드 성공: 총 {data.Count}개 항목");
            StoryProgress(); // 스토리 시작
        }
        else
        {
            Debug.LogWarning("CSV 데이터가 비어 있습니다.");
            dialogue.text = "데이터를 찾을 수 없습니다.";
        }
    }

    public void StoryProgress()
    {
        if (progress >= data.Count)
        {
            Debug.Log("모든 스토리를 완료했습니다.");
            return;
        }

        string currentChapter = data[progress]["Chapter"].ToString();
        string[] chapterParts = currentChapter.Split("_");
        dialogue_State = data[dialogue_Cnt]["Character"].ToString();

        dialogue_Box.SetActive(true);

        if (chapterParts.Length == 2)
        {
            int currentChapterNum = int.Parse(chapterParts[0]);
            int currentDialogueNum = int.Parse(chapterParts[1]);
            Debug.Log($"현재 챕터: {currentChapterNum}, 진행도: {currentDialogueNum}");
        }
        else
        {
            Debug.LogError("챕터 정보가 잘못되었습니다.");
        }

     
      
            StartCoroutine(ChapterProgress());
        
    }

    private IEnumerator ChapterProgress()
    {
        while (progress < data.Count)
        {
            // 현재 대사 출력
            string text = data[progress]["Dialogue_Korean"].ToString();
            int index = text.IndexOf("@@");

            if (index != -1)
            {
                text = text.Replace("@@", "\n");
            }

            dialogue.text = text;
            Debug.Log($"진행 중: {dialogue.text}");

            yield return new WaitForSeconds(2f); // 2초 대기

            progress++;
            dialogue_Cnt++;

            // 다음 챕터로 넘어가는 조건 체크
            if (progress < data.Count)
            {
                string nextChapter = data[progress]["Chapter"].ToString();
                if (!IsSameChapter(nextChapter))
                {
                    Debug.Log("다음 챕터로 전환");
                    yield break;
                }

                // 선택지 처리
                dialogue_State = data[dialogue_Cnt]["Character"].ToString();
                if (dialogue_State.Equals("Select"))
                {
                    ActivateSelection();
                    yield break;
                }
            }
        }

        Debug.Log("현재 챕터 완료");
        dialogue_Box.SetActive(false);
        dialogue_Obj.SetActive(false);
    }

    private bool IsSameChapter(string nextChapter)
    {
        string currentChapter = data[progress - 1]["Chapter"].ToString();
        return currentChapter.Split("_")[0] == nextChapter.Split("_")[0];
    }

    private void ActivateSelection()
    {
        dialogue_Obj.SetActive(true); // 선택지 다이얼로그 활성화
        dialogue_Box.SetActive(false); // 기존 다이얼로그 박스 비활성화

        // 선택지 텍스트 설정
        select_1.text = data[progress + 1]["Dialogue_Korean"].ToString();
        select_2.text = data[progress + 2]["Dialogue_Korean"].ToString();

        Debug.Log($"선택지 설정 완료: {select_1.text}, {select_2.text}");
    }

    public void OnSelect(int choice)
    {
        // 선택지에서 사용자가 선택한 항목 처리
        if (choice == 1)
        {
            Debug.Log($"선택지 1 선택: {select_1.text}");
            progress -=7;
        }
        else if (choice == 2)
        {
            Debug.Log($"선택지 2 선택: {select_2.text}");
            progress += 3;
        }
   
        dialogue_Obj.SetActive(false); // 선택지 다이얼로그 비활성화
        dialogue_Box.SetActive(true);
        StoryProgress(); // 다음 스토리로 진행
    }
}
