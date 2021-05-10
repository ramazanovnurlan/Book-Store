using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Interface
{
    public interface ILanguageService
    {
        void CreateLanguage(Language language, ref int errorCode, ref string result);

        void DeleteLanguageById(int languageId, ref int errorCode, ref string result);


        Language GetLanguageById(int languageId);

        void UpdateLanguage(int id, Language language, ref int errorCode, ref string result);

        List<Language> GetLanguages();
        string GetLangStr(int id);
       
    }
}
