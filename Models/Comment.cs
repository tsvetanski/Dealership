namespace Dealership.Models
{
    using Dealership.Common;
    using Dealership.Contracts;

    public class Comment : IComment
    {
        private string content;

        public Comment(string content)
        {
            this.Content = content;
        }

        public string Content
        {
            get
            {
                return this.content;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.CommentCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length, 
                    Constants.MinCommentLength,
                    Constants.MaxCommentLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Content", Constants.MinCommentLength, Constants.MaxCommentLength));
                this.content = value;
            }
        }

        public string Author { get; set; }
    }
}
