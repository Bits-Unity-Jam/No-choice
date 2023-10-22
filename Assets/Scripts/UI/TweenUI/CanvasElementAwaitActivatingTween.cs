// using UnityEngine;
//
// namespace Game.UI.Popups
// {
//     public class CanvasElementAwaitActivatingTween : BaseActivatingTweener
//     {
//         [SerializeField]
//         private EndgameScreen endgameScreen;
//
//         [ SerializeField ]
//         private bool isDataLoaded;
//         
//         private void Awake() => endgameScreen.OnDataLoaded += HandleDataLoaded;
//
//         private void HandleDataLoaded() => isDataLoaded = true;
//
//         public override void DoActivate()
//         {
//
//             if (isDataLoaded)
//             {
//                 SendTweenCompleted();
//             }
//             else
//             {
//                 endgameScreen.OnDataLoaded += SendTweenCompleted;
//             }
//         }
//
//         public override void DoDeactivate() => SendTweenCompleted();
//
//         public override void DoDeactivateImmediately() => SendTweenCompleted();
//
//         private void OnDestroy()
//         {
//             endgameScreen.OnDataLoaded -= HandleDataLoaded;
//             endgameScreen.OnDataLoaded -= SendTweenCompleted;
//         }
//     }
// }