namespace dotNet_backend.Models.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }

        //TODO: Add UpdatedAt / ModifiedAt ?
    }
}
