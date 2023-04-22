using System.ComponentModel.DataAnnotations;
using WebApi.DataTransferObjects;

namespace WebApi.Models.Entities;

public class CommentEntity
{
	[Key]
    public int CommentId { get; set; }
    public string CustomerName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Comment { get; set; } = null!;
	public DateTime DateCreated { get; set; }

	public static implicit operator CommentHttpResponse(CommentEntity entity)
	{
		return new CommentHttpResponse
		{
			CommentId = entity.CommentId,
			CustomerName = entity.CustomerName,
			Comment = entity.Comment,
			Email = entity.Email,
			DateCreated = entity.DateCreated,
		};
	}
}
