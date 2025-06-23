using AutoMapper;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();

            // Pet mappings
            CreateMap<Pet, PetDto>();
            CreateMap<CreatePetDto, Pet>();
            CreateMap<UpdatePetDto, Pet>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // PetSitter mappings
            CreateMap<PetSitter, PetSitterDto>();
            CreateMap<CreatePetSitterDto, PetSitter>();
            CreateMap<UpdatePetSitterDto, PetSitter>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // PetSitterService mappings
            CreateMap<PetSitterService, PetSitterServiceDto>();
            CreateMap<CreatePetSitterServiceDto, PetSitterService>();

            // Booking mappings
            CreateMap<Booking, BookingDto>();
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<UpdateBookingDto, Booking>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // BookingMessage mappings
            CreateMap<BookingMessage, BookingMessageDto>();
            CreateMap<CreateBookingMessageDto, BookingMessage>();

            // Review mappings
            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewDto, Review>();

            // NotificationSettings mappings
            CreateMap<UserNotificationSettings, NotificationSettingsDto>();
            CreateMap<UpdateNotificationSettingsDto, UserNotificationSettings>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}