using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CSVRoad_Story : MonoBehaviour
{
    // TextMeshProUGUI 컴포넌트를 드래그 앤 드롭으로 연결
    [SerializeField] private TextMeshProUGUI slip;

    void Start()
    {
        // CSV 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.Read("Story");

        // 첫 번째 데이터 출력 (예: Chapter = 0_1, Dialogue_Korean 출력)
        if (data.Count > 0)
        {
            Debug.Log(data[0]["Dialogue_Korean"]); // 콘솔에 출력
            slip.text = data[1]["Dialogue_Korean"].ToString(); // UI에 텍스트 설정
        }
        else
        {
            Debug.LogWarning("CSV 데이터가 비어 있습니다.");
            slip.text = "데이터를 찾을 수 없습니다.";
        }
    }
}
