using AutoMapper;
using BookShop_API.Helper;
using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.MappingProfile
{
    public class MappingEntity:Profile
    {
        public MappingEntity()
        {
            // people
            CreateMap<Checkin, AppUser>()
            .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.Phonenumber))
            .ReverseMap();
            CreateMap<AppUser, UserInfo>().ReverseMap();
            //делает перенос данных из CheckinPayload в Person. И наоборот, благодаря ReverseMap().
            //Если наименования переменных неодинаковые, как в PhoneNumber, то добавляем ForMember(......)

            // cards
            //CreateMap<CardPayload, Card>().ReverseMap();
            //CreateMap<EditCardPayload, Card>().ReverseMap();
            CreateMap<Book, BookModel>().ReverseMap();
            //CreateMap<Person, PersonInfoPayload>().ReverseMap();
        }
    }
}
