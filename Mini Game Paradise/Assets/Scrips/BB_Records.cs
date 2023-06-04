using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Linq;

public static class BB_Records
{
    public static string fileName = "BB_Records.csv";
    private class ScoreData
    {
        public int score;
        public long timestamp;

        public ScoreData(int score, long timestamp)
        {
            this.score = score;
            this.timestamp = timestamp;
        }
    }

    public static void SaveScoreToCSV(int score, long unixTime)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // CSV 파일 읽기
        List<ScoreData> scoresData = LoadDataFromCSV(filePath);

        // 현재 시간을 유닉스 타임으로 변환
        //long unixTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        // 새로운 데이터 생성
        ScoreData newData = new ScoreData(score, unixTime);

        // 리스트에 새로운 데이터 추가
        scoresData.Add(newData);

        // 점수를 기준으로 내림차순 정렬
        scoresData = scoresData.OrderByDescending(data => data.score).ToList();

        // CSV 파일 저장
        SaveDataToCSV(filePath, scoresData);

        Debug.Log("점수가 CSV 파일에 저장되었습니다.");
    }

    private static List<ScoreData> LoadDataFromCSV(string filePath)
    {
        List<ScoreData> scoresData = new List<ScoreData>();

        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // CSV 파일 열기
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');

                int score = int.Parse(data[0]);
                long timestamp = long.Parse(data[1]);

                ScoreData scoreData = new ScoreData(score, timestamp);
                scoresData.Add(scoreData);
            }

            // 파일 닫기
            sr.Close();
        }

        return scoresData;
    }

    private static void SaveDataToCSV(string filePath, List<ScoreData> scoresData)
    {
        // CSV 파일 열기 또는 생성
        StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);

        foreach (ScoreData data in scoresData)
        {
            // 점수와 시간을 CSV 형식으로 연결하여 파일에 쓰기
            string csvLine = data.score.ToString() + "," + data.timestamp.ToString();
            sw.WriteLine(csvLine);
        }

        // 파일 닫기
        sw.Close();
    }

    // 저장한 데이터 중 점수만 불러오는 함수
    public static List<int> LoadScoresFromCSV()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        List<int> scores = new List<int>();

        if (File.Exists(filePath))
        {
            // CSV 파일 열기
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');

                int score = int.Parse(data[0]);

                scores.Add(score);
            }
            // 파일 닫기
            sr.Close();
        }

        return scores;
    }

    // 저장한 데이터 중 타임스탬프만 불러오는 함수
    public static List<long> LoadTimestampsFromCSV()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // CSV 파일 열기
        StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

        List<long> timestamps = new List<long>();

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] data = line.Split(',');

            long timestamp = long.Parse(data[1]);

            timestamps.Add(timestamp);
        }

        // 파일 닫기
        sr.Close();

        return timestamps;
    }

    //public static void SaveIntToCSV(int value)
    //{
    //    string filePath = Path.Combine(Application.persistentDataPath, fileName);

    //    // CSV 파일이 이미 존재하는지 확인
    //    bool fileExists = File.Exists(filePath);

    //    // CSV 파일 열기 또는 생성
    //    StreamWriter sw = new StreamWriter(filePath, true);

    //    // int 값을 문자열로 변환하여 파일에 쓰기
    //    string csvLine = value.ToString();
    //    sw.WriteLine(csvLine);

    //    // 파일 닫기
    //    sw.Close();

    //    if (fileExists)
    //    {
    //        Debug.Log("데이터가 CSV 파일에 추가되었습니다.");
    //    }
    //    else
    //    {
    //        Debug.Log("데이터가 CSV 파일에 저장되었습니다.");
    //    }
    //}

    //public static List<int> ReadIntFromCSV()
    //{
    //    List<int> data = new List<int>();

    //    string filePath = Path.Combine(Application.persistentDataPath, fileName);

    //    // CSV 파일이 존재하는지 확인
    //    if (File.Exists(filePath))
    //    {
    //        // CSV 파일 열기
    //        StreamReader sr = new StreamReader(filePath);

    //        while (!sr.EndOfStream)
    //        {
    //            // 한 줄씩 읽어서 int 값으로 변환하여 데이터 리스트에 추가
    //            string line = sr.ReadLine();
    //            int value;
    //            if (int.TryParse(line, out value))
    //            {
    //                data.Add(value);
    //            }
    //            else
    //            {
    //                Debug.LogWarning("CSV 파일에서 int 값을 읽을 수 없습니다: " + line);
    //            }
    //        }

    //        // 파일 닫기
    //        sr.Close();

    //        Debug.Log("CSV 파일에서 데이터를 읽었습니다.");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("CSV 파일이 존재하지 않습니다.");
    //    }

    //    data.Sort(new Comparison<int>((n1, n2) => n2.CompareTo(n1)));   // 리스트 원소 내림차순
    //    return data;
    //}
}
