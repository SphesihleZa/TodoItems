using System.ComponentModel.DataAnnotations;

namespace AppFactoryTodoItem.Models
{
    public class TodoItem
    {
        [Key]
        public Guid ID { get; set; }
        public string Title { get; set; }
    }
}
