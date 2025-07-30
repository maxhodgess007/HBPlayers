using AutoMapper;
using HerveyPlayersBooking.Models;

namespace HerveyPlayersBooking.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingDto>();   // Entity → DTO
            CreateMap<BookingDto, Booking>();   // DTO → Entity (if needed)
        }
    }
}