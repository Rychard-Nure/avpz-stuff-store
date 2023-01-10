using DataLayer.Entities;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Test
{
    internal class CommentEqualityComparer : IEqualityComparer<Comment>
    {
        public bool Equals([AllowNull] Comment? x, [AllowNull] Comment? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.Body == y.Body &&
                   x.IsEdited == y.IsEdited &&
                   x.IsDeleted == y.IsDeleted &&
                   x.UserId == y.UserId &&
                   x.RepliedCommentId == y.RepliedCommentId &&
                   x.CommentedTime == y.CommentedTime &&
                   x.GameId == y.GameId;
        }

        public int GetHashCode([DisallowNull] Comment obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class GameEqualityComparer : IEqualityComparer<Game>
    {
        public bool Equals(Game? x, Game? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.Name == y.Name &&
                   x.ImagePath == y.ImagePath &&
                   x.Sold == y.Sold &&
                   x.Categories.Count() == y.Categories.Count() &&
                   x.Description == y.Description &&
                   x.IsDeleted == y.IsDeleted &&
                   x.Price == y.Price &&
                   x.GameCategoryId == y.GameCategoryId;
        }

        public int GetHashCode([DisallowNull] Game obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class GameCategoryEqualityComparer : IEqualityComparer<GameCategory>
    {
        public bool Equals(GameCategory? x, GameCategory? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] GameCategory obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class OrderEqualityComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order? x, Order? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.UserId == y.UserId &&
                   x.Comment == y.Comment &&
                   x.Payment == y.Payment &&
                   x.TotalPrice == y.TotalPrice;
        }

        public int GetHashCode([DisallowNull] Order obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class OrderDetailEqualityComparer : IEqualityComparer<OrderDetail>
    {
        public bool Equals(OrderDetail? x, OrderDetail? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.Quantity == y.Quantity &&
                   x.Price == y.Price &&
                   x.OrderId == y.OrderId &&
                   x.GameId == y.GameId;
        }

        public int GetHashCode([DisallowNull] OrderDetail obj)
        {
            return obj.GetHashCode();
        }
    }
}
