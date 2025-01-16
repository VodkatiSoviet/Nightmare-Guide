using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class CSVRoad_Story : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slip;

    [Header("스토리 진행도")]
    private List<Dictionary<string, object>> data; // CSV 파일 저장용
    private int progress = 0;    // 현재 진행도

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
            slip.text = "데이터를 찾을 수 없습니다.";
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

        // 다음 대사 실행
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
                // 이미 줄바꿈 문자가 포함된 경우 그대로 둠
                text = text.Replace("@@", "\n");
            }

            slip.text = text;
            Debug.Log($"진행 중: {slip.text}");

            yield return new WaitForSeconds(2f); // 2초 대기

            progress++;

            // 다음 챕터로 넘어가는 조건 체크
            if (progress < data.Count)
            {
                string nextChapter = data[progress]["Chapter"].ToString();
                if (!IsSameChapter(nextChapter))
                {
                    Debug.Log("다음 챕터로 전환");
                    yield break;
                }
            }
        }

        Debug.Log("현재 챕터 완료");
    }

    private bool IsSameChapter(string nextChapter)
    {
        string currentChapter = data[progress - 1]["Chapter"].ToString();
        return currentChapter.Split("_")[0] == nextChapter.Split("_")[0];
    }
}
