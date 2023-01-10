using AutoMapper;
using BLL.Models;
using DataLayer.Entities;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<GameCategory, GameCategoryModel>()
                .ForMember(rm => rm.CategoryGamesIds, r => r.MapFrom(g => g.Games.Select(i => i.GameId)))
                .ReverseMap();

            CreateMap<Game, GameModel>()
                .ForMember(rm => rm.Categories, r => r.MapFrom(i => i.Categories.Select(c => new GameCategoryModel()
                {
                    Id = c.Category.Id,
                    Name = c.Category.Name
                })));

            CreateMap<GameModel, Game>()
                   .ForMember(rm => rm.Categories, r => r.MapFrom(i => i.Categories.Select(c => new CategoryGame()
                   {
                       CategoryId = c.Id,
                       GameId = i.Id
                   })));

            CreateMap<Order, OrderModel>()
                .ForMember(rm => rm.OrderDetailIds, r => r.MapFrom(i => i.OrderDetails.Select(o => o.Id)))
                .ReverseMap();
        }
    }
}
