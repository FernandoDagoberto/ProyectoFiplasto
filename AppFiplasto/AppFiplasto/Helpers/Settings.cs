namespace AppFiplasto.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        private const string usuario = "usuario";
        private const string isRemember = "isRemember";
        private const string permiso = "permiso";

        private static readonly string stringDefault = string.Empty;
        private static readonly bool boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Usuario
        {
            get => AppSettings.GetValueOrDefault(usuario, stringDefault);
            set => AppSettings.AddOrUpdateValue(usuario, value);
        }

        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(isRemember, boolDefault);
            set => AppSettings.AddOrUpdateValue(isRemember, value);
        }

        public static string Permiso
        {
            get => AppSettings.GetValueOrDefault(permiso, stringDefault);
            set => AppSettings.AddOrUpdateValue(permiso, value);
        }




    }
}
