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

        // CSV ���� �б�
        List<ScoreData> scoresData = LoadDataFromCSV(filePath);

        // ���� �ð��� ���н� Ÿ������ ��ȯ
        //long unixTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        // ���ο� ������ ����
        ScoreData newData = new ScoreData(score, unixTime);

        // ����Ʈ�� ���ο� ������ �߰�
        scoresData.Add(newData);

        // ������ �������� �������� ����
        scoresData = scoresData.OrderByDescending(data => data.score).ToList();

        // CSV ���� ����
        SaveDataToCSV(filePath, scoresData);

        Debug.Log("������ CSV ���Ͽ� ����Ǿ����ϴ�.");
    }

    private static List<ScoreData> LoadDataFromCSV(string filePath)
    {
        List<ScoreData> scoresData = new List<ScoreData>();

        // ������ �����ϴ��� Ȯ��
        if (File.Exists(filePath))
        {
            // CSV ���� ����
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

            // ���� �ݱ�
            sr.Close();
        }

        return scoresData;
    }

    private static void SaveDataToCSV(string filePath, List<ScoreData> scoresData)
    {
        // CSV ���� ���� �Ǵ� ����
        StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);

        foreach (ScoreData data in scoresData)
        {
            // ������ �ð��� CSV �������� �����Ͽ� ���Ͽ� ����
            string csvLine = data.score.ToString() + "," + data.timestamp.ToString();
            sw.WriteLine(csvLine);
        }

        // ���� �ݱ�
        sw.Close();
    }

    // ������ ������ �� ������ �ҷ����� �Լ�
    public static List<int> LoadScoresFromCSV()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        List<int> scores = new List<int>();

        if (File.Exists(filePath))
        {
            // CSV ���� ����
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');

                int score = int.Parse(data[0]);

                scores.Add(score);
            }
            // ���� �ݱ�
            sr.Close();
        }

        return scores;
    }

    // ������ ������ �� Ÿ�ӽ������� �ҷ����� �Լ�
    public static List<long> LoadTimestampsFromCSV()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // CSV ���� ����
        StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

        List<long> timestamps = new List<long>();

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] data = line.Split(',');

            long timestamp = long.Parse(data[1]);

            timestamps.Add(timestamp);
        }

        // ���� �ݱ�
        sr.Close();

        return timestamps;
    }

    //public static void SaveIntToCSV(int value)
    //{
    //    string filePath = Path.Combine(Application.persistentDataPath, fileName);

    //    // CSV ������ �̹� �����ϴ��� Ȯ��
    //    bool fileExists = File.Exists(filePath);

    //    // CSV ���� ���� �Ǵ� ����
    //    StreamWriter sw = new StreamWriter(filePath, true);

    //    // int ���� ���ڿ��� ��ȯ�Ͽ� ���Ͽ� ����
    //    string csvLine = value.ToString();
    //    sw.WriteLine(csvLine);

    //    // ���� �ݱ�
    //    sw.Close();

    //    if (fileExists)
    //    {
    //        Debug.Log("�����Ͱ� CSV ���Ͽ� �߰��Ǿ����ϴ�.");
    //    }
    //    else
    //    {
    //        Debug.Log("�����Ͱ� CSV ���Ͽ� ����Ǿ����ϴ�.");
    //    }
    //}

    //public static List<int> ReadIntFromCSV()
    //{
    //    List<int> data = new List<int>();

    //    string filePath = Path.Combine(Application.persistentDataPath, fileName);

    //    // CSV ������ �����ϴ��� Ȯ��
    //    if (File.Exists(filePath))
    //    {
    //        // CSV ���� ����
    //        StreamReader sr = new StreamReader(filePath);

    //        while (!sr.EndOfStream)
    //        {
    //            // �� �پ� �о int ������ ��ȯ�Ͽ� ������ ����Ʈ�� �߰�
    //            string line = sr.ReadLine();
    //            int value;
    //            if (int.TryParse(line, out value))
    //            {
    //                data.Add(value);
    //            }
    //            else
    //            {
    //                Debug.LogWarning("CSV ���Ͽ��� int ���� ���� �� �����ϴ�: " + line);
    //            }
    //        }

    //        // ���� �ݱ�
    //        sr.Close();

    //        Debug.Log("CSV ���Ͽ��� �����͸� �о����ϴ�.");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("CSV ������ �������� �ʽ��ϴ�.");
    //    }

    //    data.Sort(new Comparison<int>((n1, n2) => n2.CompareTo(n1)));   // ����Ʈ ���� ��������
    //    return data;
    //}
}
