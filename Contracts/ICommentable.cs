namespace Dealership.Contracts
{
    using System.Collections.Generic;

    public interface ICommentable
    {
        IList<IComment> Comments { get; }
    }
}
