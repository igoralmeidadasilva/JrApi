using System.Text.RegularExpressions;

namespace JrApi.SharedKernel.Extensions;

public static class StringExtensions
{
    public static string RemoveAccents(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        Dictionary<char, char> map = new()
        {
            {'á', 'a'}, {'à', 'a'}, {'â', 'a'}, {'ä', 'a'}, {'ã', 'a'}, {'å', 'a'},
            {'é', 'e'}, {'è', 'e'}, {'ê', 'e'}, {'ë', 'e'},
            {'í', 'i'}, {'ì', 'i'}, {'î', 'i'}, {'ï', 'i'},
            {'ó', 'o'}, {'ò', 'o'}, {'ô', 'o'}, {'ö', 'o'}, {'õ', 'o'},
            {'ú', 'u'}, {'ù', 'u'}, {'û', 'u'}, {'ü', 'u'},
            {'ç', 'c'},
            {'Á', 'A'}, {'À', 'A'}, {'Â', 'A'}, {'Ä', 'A'}, {'Ã', 'A'}, {'Å', 'A'},
            {'É', 'E'}, {'È', 'E'}, {'Ê', 'E'}, {'Ë', 'E'},
            {'Í', 'I'}, {'Ì', 'I'}, {'Î', 'I'}, {'Ï', 'I'},
            {'Ó', 'O'}, {'Ò', 'O'}, {'Ô', 'O'}, {'Ö', 'O'}, {'Õ', 'O'},
            {'Ú', 'U'}, {'Ù', 'U'}, {'Û', 'U'}, {'Ü', 'U'},
            {'Ç', 'C'}
        };

        Regex regexAcentos = new("[áàâäãåéèêëíìîïóòôöõúùûüçÁÀÂÄÃÅÉÈÊËÍÌÎÏÓÒÔÖÕÚÙÛÜÇ]");

        return regexAcentos.Replace(text, m => map[m.Value[0]].ToString());
    }
}