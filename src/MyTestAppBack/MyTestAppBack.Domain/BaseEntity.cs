using System.ComponentModel.DataAnnotations;

namespace MyTestAppBack.Domain
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
