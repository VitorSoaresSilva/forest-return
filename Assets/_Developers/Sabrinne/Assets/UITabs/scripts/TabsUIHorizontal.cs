namespace _Developers.Sabrinne.Assets.UITabs.scripts
{
    public class TabsUIHorizontal : TabsUI
    {
#if UNITY_EDITOR
        private void Reset() {
            OnValidate();
        }
        private void OnValidate() {
            base.Validate(TabsType.Horizontal);
        }
#endif
    }
}
