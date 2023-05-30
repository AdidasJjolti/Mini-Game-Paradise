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

        // CSV ������ �̹� �����ϴ��� Ȯ��
        bool fileExists = File.Exists(filePath);

        // CSV ���� ���� �Ǵ� ����
        StreamWriter sw = new StreamWriter(filePath, true);

        // int ���� ���ڿ��� ��ȯ�Ͽ� ���Ͽ� ����
        string csvLine = value.ToString();
        sw.WriteLine(csvLine);

        // ���� �ݱ�
        sw.Close();

        if (fileExists)
        {
            Debug.Log("�����Ͱ� CSV ���Ͽ� �߰��Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("�����Ͱ� CSV ���Ͽ� ����Ǿ����ϴ�.");
        }
    }

    public List<int> ReadIntFromCSV()
    {
        List<int> data = new List<int>();

        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // CSV ������ �����ϴ��� Ȯ��
        if (File.Exists(filePath))
        {
            // CSV ���� ����
            StreamReader sr = new StreamReader(filePath);

            while (!sr.EndOfStream)
            {
                // �� �پ� �о int ������ ��ȯ�Ͽ� ������ ����Ʈ�� �߰�
                string line = sr.ReadLine();
                int value;
                if (int.TryParse(line, out value))
                {
                    data.Add(value);
                }
                else
                {
                    Debug.LogWarning("CSV ���Ͽ��� int ���� ���� �� �����ϴ�: " + line);
                }
            }

            // ���� �ݱ�
            sr.Close();

            Debug.Log("CSV ���Ͽ��� �����͸� �о����ϴ�.");
        }
        else
        {
            Debug.LogWarning("CSV ������ �������� �ʽ��ϴ�.");
        }

        data.Sort(new Comparison<int>((n1, n2) => n2.CompareTo(n1)));   // ����Ʈ ���� ��������
        return data;
    }
}
