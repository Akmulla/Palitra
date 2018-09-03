using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenUrlText : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI Label;


    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(Label, Input.mousePosition, null);
        if( linkIndex != -1 ) { // was a link clicked?
            TMP_LinkInfo linkInfo = Label.textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field

            var linkId = linkInfo.GetLinkID();
            if (Uri.IsWellFormedUriString(linkId, UriKind.Absolute))
            {
                Application.OpenURL(linkId);
            }
            else
            {
                switch (linkId)
                {
                        case "Feedback":
                            FeedbackEmail.Send();
                            break;
                        default:
                            Debug.LogError($"Don't know what to do with: {linkId}");
                            break;
                }
            }
        }
    }
}