using System;
//using UnityEngine.Localization.Settings;

public class LangManager
{
    private static string _currLang;
    public static event Action<string> OnLangChange;

    public static string defaultLang
    {
        get { return "ru"; }
    }

    public static string currLang
    {
        get {
            return _currLang;
        }
        set {
            switch (value.ToLower()) {
                case "ru":
                    _currLang = "ru";
                    break;
                case "en":
                    _currLang = "en";
                    break;
                default:
                    _currLang = "ru";
                    break;
            }
            SelectLocale(_currLang);
            OnLangChange?.Invoke(_currLang);
        }
    }

    public static void SelectLocale(string lang)
    {
        /*
        var locales = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < locales.Count; i++)
        {
            if (locales[i].Identifier.Code.Equals(lang.ToLower()))
            {
                LocalizationSettings.SelectedLocale = locales[i];
                return;
            }
        }
        */
    }
}
