using WebApi.Models.Entities;

namespace WebApi.DataTransferObjects;

public class CommentHttpRequest
{
	public string CustomerName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Comment { get; set; } = null!;

	public static implicit operator CommentEntity(CommentHttpRequest request)
	{
		return new CommentEntity
		{
			CustomerName = request.CustomerName,
			Email = request.Email,
			Comment = request.Comment,
		};
	}
}
