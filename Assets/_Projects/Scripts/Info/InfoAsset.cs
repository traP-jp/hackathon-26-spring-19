    using UnityEngine;

    /// <summary>
    /// UI などに表示する静的な情報の基底クラス。
    /// ゲーム進行中に変化する値は保持しない。
    /// </summary>
    public abstract class InfoAsset : ScriptableObject
    {
        [SerializeField]
        private string id = string.Empty;

        [SerializeField]
        private string displayName = string.Empty;

        [SerializeField, TextArea]
        private string description = string.Empty;

        [SerializeField]
        private Sprite icon;

        public string Id => id;
        public string DisplayName => displayName;
        public string Description => description;
        public Sprite Icon => icon;

        protected virtual void OnValidate()
        {
            id = (id ?? string.Empty).Trim();
            displayName = (displayName ?? string.Empty).Trim();
        }
    }
