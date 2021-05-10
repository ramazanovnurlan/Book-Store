using BookShop_API.BookShop_Repostory.Repostory;
using BookShop_API.BookShopServices.AppConfig;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Implementation
{
    public class LanguageService : ILanguageService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly IRepository<Language> _languages;
        private readonly IRepository<Errors> _errors;
        //    private readonly ILoggerManager _logger;


        public LanguageService(IRepository<Language> languages,
            IRepository<Errors> errors
            )
        {
            _languages = languages;
            _errors = errors;
        }
        public void CreateLanguage(Language language, ref int errorCode, ref string result)
        {
            _languages.Insert(language);
            _languages.Save();
            result = "Uğurla başa çatdı!";
            errorCode = 0;
        }

        public void DeleteLanguageById(int languageId, ref int errorCode, ref string result)
        {
            Language language = _languages.FindBy(x => x.Id == languageId).FirstOrDefault();
            if (language == null)
            {
                result = "Silmek ucun lazimi melumat tapilmadi!";
                errorCode = 0;
            }
            else
            {
                _languages.Remove(language);
                _languages.Save();
                result = "Uğurla başa çatdı!";
                errorCode = 0;
            }
        }

        public Language GetLanguageById(int languageId)
        {
            return _languages.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == languageId);
        }

        public  List<Language> GetLanguages()
        {
            return  _languages.AllQuery.ToList();
        }

        public void UpdateLanguage(int id, Language language, ref int errorCode, ref string result)
        {
            try
            {
                var oldlanguage = _languages.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (oldlanguage == null)
                {
                    errorCode = 1;
                    result = "Language tapılmadı!";
                    return;
                }
                oldlanguage.Title = language.Title;
                _languages.Update(oldlanguage);
                _languages.Save();
                errorCode = 0;
                result = "Uğurla başa çatdı!";
            }
            catch (Exception)
            {
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }
        public string GetLangStr(int id)
        {
            String str = _languages.FindBy(x=>x.Id==id).FirstOrDefault().Title;
            return str;
           
        }
    }
}
