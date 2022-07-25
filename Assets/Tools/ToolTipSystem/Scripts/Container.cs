using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Tooltip transform that holds the set text

namespace ToolTip
{
    [ExecuteInEditMode()]
    public class Container : MonoBehaviour
    {
        public RectTransform rt;
        public TextMeshProUGUI headerField, contentField;
        public LayoutElement layout;
        public int characterWrapLimit;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
        }

        //properly display text and size if necessary
        public void SetText(string content, string header = "")
        {
            //remove header if no header provided
            if (string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }

            contentField.text = content;

            //activate layout if content or header exceeds the set limit
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layout.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        public void UpdatePosition()
        {
            //set position of tooltip to where the mouse is
            Vector2 mousePosition = Input.mousePosition;
            transform.position = mousePosition;

            //adjust the tooltip pivot so that it never appears off screen
            //set according to mouseposition screen percentage
            float pivotX = mousePosition.x / Screen.width;
            float pivotY = mousePosition.y / Screen.height;
            rt.pivot = new Vector2(pivotX, pivotY);
        }
    }
}

