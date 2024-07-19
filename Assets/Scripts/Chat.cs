using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private GameObject userMessagePrefab;
    [SerializeField] private GameObject responseMessagePrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private ScrollRect scrollChat;

    private bool canSendMessage = true;

    public void OnSendMessage()
    {
        if (!canSendMessage || string.IsNullOrWhiteSpace(inputField.text))
            return;

        string messageText = inputField.text;
        inputField.text = "";
        canSendMessage = false;

        StartCoroutine(HandleMessageSending(messageText));
    }

    private IEnumerator HandleMessageSending(string messageText)
    {
        DisplayUserMessage(messageText);
        yield return new WaitForSeconds(0.1f);
        ScrollToBottom();

        yield return new WaitForSeconds(0.8f);

        DisplayResponseMessage("(^-^) (^.^)");
        yield return new WaitForSeconds(0.1f);
        ScrollToBottom();

        canSendMessage = true;
    }

    private void DisplayUserMessage(string messageText)
    {
        GameObject message = Instantiate(userMessagePrefab, Vector3.zero, Quaternion.identity, content.transform);
        message.GetComponent<Message>().message.text = messageText;
    }

    private void DisplayResponseMessage(string messageText)
    {
        GameObject message = Instantiate(responseMessagePrefab, Vector3.zero, Quaternion.identity, content.transform);
        message.GetComponent<Message>().message.text = messageText;
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollChat.verticalNormalizedPosition = 0;
    }
}
