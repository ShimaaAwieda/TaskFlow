using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<TaskItem> TaskItems { get; set; }
    }
}