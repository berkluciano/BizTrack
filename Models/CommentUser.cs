namespace UniProject1.Models
{
    public class CommentUser
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; }=null!;
    }
}
