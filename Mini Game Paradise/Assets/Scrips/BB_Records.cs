using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BB_Records : MonoBehaviour
{
    public string fileName = "BB_Records.csv";

    public void SaveIntToCSV(int value)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // CSV 파일이 이미 존재하는지 확인
        bool fileExists = File.Exists(filePath);

        // CSV 파일 열기 또는 생성
        StreamWriter sw = new StreamWriter(filePath, true);

        // int 값을 문자열로 변환하여 파일에 쓰기
        string csvLine = value.ToString();
        sw.WriteLine(csvLine);

        // 파일 닫기
        sw.Close();

        if (fileExists)
        {
            Debug.Log("데이터가 CSV 파일에 추가되었습니다.");
        }
        else
        {
            Debug.Log("데이터가 CSV 파일에 저장되었습니다.");
        }
    }

    public List<int> ReadIntFromCSV()
    {
        List<int> data = new List<int>();

        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // CSV 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // CSV 파일 열기
            StreamReader sr = new StreamReader(filePath);

            while (!sr.EndOfStream)
            {
                // 한 줄씩 읽어서 int 값으로 변환하여 데이터 리스트에 추가
                string line = sr.ReadLine();
                int value;
                if (int.TryParse(line, out value))
                {
                    data.Add(value);
                }
                else
                {
                    Debug.LogWarning("CSV 파일에서 int 값을 읽을 수 없습니다: " + line);
                }
            }

            // 파일 닫기
            sr.Close();

            Debug.Log("CSV 파일에서 데이터를 읽었습니다.");
        }
        else
        {
            Debug.LogWarning("CSV 파일이 존재하지 않습니다.");
        }

        data.Sort(new Comparison<int>((n1, n2) => n2.CompareTo(n1)));   // 리스트 원소 내림차순
        return data;
    }
}
