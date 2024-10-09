using UnityEngine;
namespace Dialogue
{
    [CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/Dialogue/DialogueSO")]
    public class DialogueSO : ScriptableObject
    {
        public DialogueLine[] Lines;
        public bool TryGetDialogueLineAt(int index, out DialogueLine line)
        {
            if (index < 0 || index >= Lines.Length)
            {
                line = null;
                return false;
            }

            line = Lines[index];
            return true;
        }
    }
}