using AutoMapper;
using BLL;
using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Identity.Data;
using Web.Data;

namespace GameStore.Test
{
    public class UnitTestHelper
    {
        public static DbContextOptions<GameStoreDBContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<GameStoreDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var options2 = new DbContextOptionsBuilder<WebContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new GameStoreDBContext(options))
            {
                SeedData(context);
            }
            //using (var context = new WebContext(options2))
            //{
            //    SeedIdentityData(context);
            //}

            return options;
        }

        static string guidId1 = "99f69b1a-a567-4c79-b0e3-0e5d3e10f254";
        static string guidId2 = "a872b98b-6e1b-47e8-bd3d-c9cea94c5759";
        //public static List<WebUser> users = new List<WebUser>()
        //    {
        //        new WebUser()
        //        {
        //            Id = guidId1,
        //            FirstName = "TestUser1",
        //            LastName = "l1",
        //            Email = "email1@gmail.com",
        //            AvataraPath = "image1.jpg",
        //            PhoneNumber = "222222222",
        //            PasswordHash = "user1234"
        //        },
        //        new WebUser()
        //        {
        //            Id = guidId2,
        //            FirstName = "TestUser2",
        //            LastName = "l2",
        //            Email = "email2@gmail.com",
        //            AvataraPath = "image2.jpg",
        //            PhoneNumber = "111111111",
        //            PasswordHash = "user1234"
        //        },
        //    };
        public static List<Comment> comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Body = "body1",
                    CommentedTime= DateTime.Now.ToString(),
                    RepliedCommentId = 0,
                    GameId = 1,
                    IsDeleted = false,
                    IsEdited= false,
                    UserId = guidId1
                },
                new Comment()
                {
                    Id = 2,
                    Body = "body2",
                    CommentedTime= DateTime.Now.ToString(),
                    RepliedCommentId = 1,
                    GameId = 1,
                    IsDeleted = false,
                    IsEdited= false,
                    UserId = guidId2
                },
                new Comment()
                {
                    Id = 3,
                    Body = "body3",
                    CommentedTime= DateTime.Now.ToString(),
                    RepliedCommentId = 0,
                    GameId = 2,
                    IsDeleted = true,
                    IsEdited= false,
                    UserId = guidId2
                }
            };
        public static List<GameCategory> categories = new List<GameCategory>()
            {
                new GameCategory() { Id = 1, Name = "Advanture"},
                new GameCategory() { Id = 2, Name = "Races"},
                new GameCategory() { Id = 3, Name = "Strategy"},
                new GameCategory() { Id = 4, Name = "Rpg"},
                new GameCategory() { Id = 5, Name = "Action"},
                new GameCategory() { Id = 6, Name = "Puzzle & skill"},
                new GameCategory() { Id = 7, Name = "Other"}
            };
        public static List<Game>? games = new List<Game>()
            {
                new Game()
                {
                    Id = 1,
                    Name = "PUBG",
                    Price = 0,
                    Description = "Gameplay. PUBG is a player versus player shooter game in which up to one hundred players fight in a battle royale, a type of large-scale last man standing deathmatch where players fight to remain the last alive.",
                    ImagePath = "images/pubg.jpg",
                    IsDeleted = false,
                    Sold = 123,
                    GameCategoryId = 0,
                    Categories = new List<CategoryGame>()
                    {
                        new CategoryGame() {Category = categories[0]},
                        new CategoryGame() {Category = categories[1]}
                    }
                },
                new Game()
                {
                    Id = 2,
                    Name = "World War III",
                    Price = 150.0m,
                    Description = "Gameplay player versus player shooter game in which up to one hundred players fight in a battle royale, a type of large-scale last man standing deathmatch where players fight to remain the last alive.",
                    ImagePath = "images/big.jpg",
                    IsDeleted = false,
                    Sold = 13,
                    GameCategoryId = 0,
                    Categories = new List<CategoryGame>()
                    {
                        new CategoryGame() {Category = categories[1]},
                        new CategoryGame() {Category = categories[2]}
                    }
                },
                new Game()
                {
                    Id = 3,
                    Name = "Need for Speed",
                    Price = 99.0m,
                    Description = "The series generally centers around illicit street racing and tasks players to complete various types of races while evading the local law enforcement in police pursuits.",
                    ImagePath = "images/race.jpg",
                    IsDeleted = false,
                    Sold = 1212,
                    GameCategoryId = 0,
                    Categories = new List<CategoryGame>()
                    {
                        new CategoryGame() {Category = categories[0]},
                        new CategoryGame() {Category = categories[2]},
                        new CategoryGame() {Category = categories[3]}
                    }
                }
            };
        public static List<OrderDetail> orderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    Id = 1,
                    GameId= 1,
                    OrderId = 1,
                    Price = 30,
                    Quantity = 2
                },
                new OrderDetail()
                {
                    Id = 2,
                    GameId= 3,
                    OrderId = 1,
                    Price = 40,
                    Quantity = 1
                }
            };
        public static List<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Comment = "comment1",
                    Payment = PaymentType.Card,
                    TotalPrice = 100,
                    UserId = guidId1,
                    OrderDetails = orderDetails.GetRange(0, 2)
                }
            };

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        private static void SeedData(GameStoreDBContext context)
        {
            context.GameCategories.AddRange(categories);
            context.Games.AddRange(games);
            context.Comments.AddRange(comments);
            context.Orders.AddRange(orders);
            context.OrderDetails.AddRange(orderDetails);
            context.SaveChanges();
            //context.CategoryGames.AddRange(categoryGames);
        }

        //private static void SeedIdentityData(WebContext context)
        //{
        //    context.Users.AddRange(users);
        //}
    }
}
