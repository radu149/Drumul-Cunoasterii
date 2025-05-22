using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class ElocutionChat : MonoBehaviour
{
    public TMP_Text TMP_QuestionText;
    public TMP_InputField InputField_Answer;
    public TMP_Text TMP_ResponseText;
    public Button Button_Submit;
    public Button Button_Quit;
    public Image ActiveMechanicPanel;

    public PlayerDataPlace PlayerDataPlace;

    private string[] questions = new string[]
    {
//        "Esti bine asa cum esti?",
        "Imagineaza-ti ca esti la un interviu. Cum te-ai prezenta in 3-4 propozitii?",
        "Convinge un prieten sa participe la un proiect cu tine. Ce i-ai spune?",
        "Explica unui copil de 6 ani ce este inteligenta artificiala.",
        "Descrie o situatie in care ai reusit sa rezolvi un conflict intre doua persoane.",
        "Povesteste un moment in care ai invatat ceva important dintr-un esec."
    };

    private int currentQuestionIndex = 0;
    private bool inQuizMode = true;

    private const string apiUrl = "http://localhost:11434/api/generate";

    void Start()
    {
        Button_Submit.onClick.AddListener(OnSubmit);
        Button_Quit.onClick.AddListener(OnQuitClicked);

        TMP_QuestionText.text = questions[currentQuestionIndex];
    }

    void OnSubmit()
    {
        string answer = InputField_Answer.text.Trim();
        if (string.IsNullOrEmpty(answer)) return;

        InputField_Answer.text = "";
        TMP_ResponseText.text = "";

        if (inQuizMode)
        {
            string prompt = $"Intrebare: {questions[currentQuestionIndex]}\nRaspuns utilizator: {answer}\n" +
                            "Evalueaza raspunsul pe o scara de la 1 la 10 si ofera o sugestie de imbunatatire cu un exemplu. " +
                            "Raspunsul trebuie sa fie exclusiv in limba romana. Format:\nScor: [x/10]\nSugestie: [text]";
            StartCoroutine(SendStreamingWithTypewriter(prompt, true));
        }
        else
        {
            string prompt = $"Raspunde exclusiv in limba romana.\nContinua conversatia cu utilizatorul.\nUtilizator: {answer}";
            StartCoroutine(SendStreamingWithTypewriter(prompt, false));
        }
    }

    IEnumerator SendStreamingWithTypewriter(string prompt, bool isQuiz)
    {
        string jsonBody = $"{{\"model\":\"llama3\",\"prompt\":\"{EscapeJson(prompt)}\",\"stream\":true}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        request.SendWebRequest();

        string currentText = isQuiz ? "" : "AI: ";
        TMP_ResponseText.text = currentText;

        int lastLength = 0;

        while (!request.isDone)
        {
            string partial = request.downloadHandler.text;
            string parsed = ExtractAllResponses(partial);

            if (parsed.Length > lastLength)
            {
                string newChunk = parsed.Substring(lastLength);
                lastLength = parsed.Length;
                yield return StartCoroutine(TypeText(newChunk));
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success && isQuiz)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Length)
            {
                TMP_QuestionText.text = questions[currentQuestionIndex];
            }
            else
            {
                inQuizMode = false;
                TMP_QuestionText.text = "Exercitiul s-a terminat! Poti vorbi liber cu AI-ul.";
                if (PlayerDataPlace.Instance.Level3CoinsInactive)
                {
                    PlayerDataPlace.Instance.coinCount += 50;
                    PlayerDataPlace.Instance.Levels += 1;
                }
                PlayerDataPlace.Instance.Level3CoinsInactive = false;
            }
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            TMP_ResponseText.text = "Eroare: " + request.error;
        }
    }

    IEnumerator TypeText(string text, float delay = 0.02f)
    {
        foreach (char c in text)
        {
            TMP_ResponseText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    string ExtractAllResponses(string json)
    {
        StringBuilder sb = new StringBuilder();
        string[] lines = json.Split(new[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            int start = line.IndexOf("\"response\":\"");
            if (start >= 0)
            {
                start += "\"response\":\"".Length;
                int end = line.IndexOf("\"", start);
                if (end > start)
                {
                    string part = line.Substring(start, end - start);
                    part = part.Replace("\\n", "\n").Replace("\\\"", "\"");
                    sb.Append(part);
                }
            }
        }

        return sb.ToString();
    }

    string EscapeJson(string input)
    {
        return input.Replace("\"", "\\\"").Replace("\n", "\\n");
    }

    void OnQuitClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
