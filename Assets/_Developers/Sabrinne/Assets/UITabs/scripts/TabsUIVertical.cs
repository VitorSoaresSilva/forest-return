namespace _Developers.Sabrinne.Assets.UITabs.scripts
{
    public class TabsUIVertical : TabsUI
    {
#if UNITY_EDITOR
        private void OnValidate() {
            base.Validate(TabsType.Vertical);
        }
#endif
    }
}
