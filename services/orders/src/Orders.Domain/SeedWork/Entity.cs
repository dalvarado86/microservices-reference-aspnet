﻿namespace Orders.Domain.SeedWork
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
