using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppelsDOffresApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        [Required]
        public string FromUserId { get; set; }  // Ensure this is not nullable
        public string? ToUserId { get; set; }    // Ensure this is not nullable for private messages

        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public bool IsPrivate { get; set; }

        // Navigation properties
        public virtual Utilisateur FromUser { get; set; }
        public virtual Utilisateur ToUser { get; set; }
    }

    }
