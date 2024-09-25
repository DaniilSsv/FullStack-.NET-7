using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.ViewModels;

namespace FullstackOpdracht.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Stadium
            CreateMap<Stadium, StadiumVM>();
            
            //Team
            CreateMap<Team, TeamVM>();
            CreateMap<Team, TeamVM>()
            .ForMember(dest => dest.MatchAwayTeamIds, opt => opt.MapFrom(src => src.MatchAwayTeams.Select(m => m.Id)))
            .ForMember(dest => dest.MatchHomeTeamIds, opt => opt.MapFrom(src => src.MatchHomeTeams.Select(m => m.Id)))
            .ForMember(dest => dest.MembershipIds, opt => opt.MapFrom(src => src.Memberships.Select(m => m.Id)))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => 300)); 

            //Match;
            CreateMap<Match, MatchVM>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam));

            //section
            CreateMap<Section, CreateTicketVM>();

            //Booking
            CreateMap<BookingMembership, CartVM>()
            .ForMember(dest => dest.Prijs, opt => opt.MapFrom(src => src.Membership.Price));

            CreateMap<BookingTicket, CartVM>()
            .ForMember(dest => dest.Prijs, opt => opt.MapFrom(src => src.Ticket.Price));

            CreateMap<CreateTicketVM, Section>();

            CreateMap<AspNetUser, UserVM>();

            CreateMap<Ticket, TicketVM>();
        }
    }
}
