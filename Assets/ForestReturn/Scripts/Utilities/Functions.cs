namespace Utilities
{
    public class Functions
    {
        public static string GetSaveFileName(Enums.DataTypeSave dataTypeSave)
        {
            return dataTypeSave + ".data_version1";
        }
    }
}