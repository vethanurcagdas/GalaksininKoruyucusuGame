using System.Collections;
using UnityEngine;

namespace UI.Scenes
{
    public class BeginningScene : UIScene
    {
        public IEnumerator InitializeRoutine()
        {
            yield return new WaitForSeconds(0.3f);
            Open();       
        }

        public override void SkipStep()
        {
        }

        public override void PlayTutorial()
        {
        }
    }
}