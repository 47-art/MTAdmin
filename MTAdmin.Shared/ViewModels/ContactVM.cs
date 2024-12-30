using MTAdmin.Shared.Models;
using System;

namespace MTAdmin.Shared.ViewModels
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class ContactParameters : QueryStringParameters
    {

    }
    public class ContactMessageVM
    {
        public string Message { get; set; }
    }
}
